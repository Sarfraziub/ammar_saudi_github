using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Users;
using Application.Features.WhatsAppMessaging.Command.SendPaymentDoneMessage;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.User.Commands.Login.V1;

public class Handler : IRequestHandler<LoginCommand, LoginResult>
{
	private readonly IApplicationDbContext _context;
	private readonly IDateTime _dateTime;
	private readonly IConfiguration _configuration;
	private readonly IJwtAuthManager _jwtAuthManager;
	private readonly IUserManager _userManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;


    public Handler(
		IUserManager userManager
		, IJwtAuthManager jwtAuthManager
		, IApplicationDbContext context
		, IDateTime dateTime
		, IConfiguration configuration
        , ICurrentUserService currentUserService
        , IMapper mapper, 
        IMediator mediator)
	{
		_userManager = userManager;
		_jwtAuthManager = jwtAuthManager;
		_context = context;
		_dateTime = dateTime;
		_configuration = configuration;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _mediator = mediator;
    }

	public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
	{

        bool verifyStatus;
        var sendMessage = false;
        var guestOrders = new List<GuestOrderVm>();

        if ((request.PhoneNumber is "00966700000000" or "00966700000001" or "00966700000002"))
        {
            verifyStatus = true;
        }
        //else if (bool.Parse(_configuration["development"]) && request.Token == "1234")
        //{
        //    verifyStatus = true;
        //}
        else
        {
            //verifyStatus = await _userManager.VerifyTwoFactorSixDigitsTokenAsync(
            //	user, request.Token)
            verifyStatus = await _userManager.VerifyOtp(request.PhoneNumber, request.Token);
        }

        if (!verifyStatus) throw new AppBadRequestException("Invalid Token");


        var user = await _userManager.FindByPhoneAsync(request.PhoneNumber);
        //var user = await _context.ApplicationUsers.FirstOrDefaultAsync(x =>
        //    x.PhoneNumber == request.PhoneNumber && x.Active == true,cancellationToken);

        

        if (user == null && _currentUserService.Role == ApplicationRoles.Guest.ToString())
        {
            var identityResult =
                await _userManager.CreateUserAsync(new CreateUserModel(request.PhoneNumber, ApplicationRoles.User,
                    false));
            if (!identityResult.Succeeded) throw new AppBadRequestException("Error");

        }
        else if (user == null && _currentUserService.UserId == null)
        {
            var identityResult =
                await _userManager.CreateUserAsync(new CreateUserModel(request.PhoneNumber, ApplicationRoles.User,
                    false));
            if (!identityResult.Succeeded) throw new AppBadRequestException("Error");
        }

        if (bool.Parse(_configuration["development"]))
        {
            //if (user.UserName is "00966700000000" or "00966700000001" or "00966700000002")
            //{
            //	return userId;
            //}
        }

        //user = await _context.ApplicationUsers.FirstOrDefaultAsync(x =>
        //    x.PhoneNumber == request.PhoneNumber && x.Active == true, cancellationToken);
        user = await _userManager.FindByPhoneAsync(request.PhoneNumber);

        if (user == null) throw new AppNotFoundException("User not found");
        if (user.LockoutEnd != null) throw new AppBadRequestException("Account Locked");

        var clientOrders = await _context
            .ClientOrders
            .Where(w => w.ClientId == _currentUserService.UserId && w.Active == 1 && w.ClientOrderStatus == ClientOrderStatuses.PaymentReceived)
            .ToListAsync(cancellationToken);

        if(clientOrders.Count() == 1 && DateTime.UtcNow < clientOrders.First().Updated?.AddHours(24))
        {
            clientOrders.First().ClientId = user.Id;
            sendMessage = true;
        }
        else
        {
            guestOrders = _mapper.Map<List<GuestOrderVm>>(clientOrders);
        }

        if (user == null) throw new AppBadRequestException("Error");

        if (!user.PhoneNumberConfirmed) user.PhoneNumberConfirmed = true;

        if (user.FirstLogin == null)
        {
            user.FirstLogin = true;
            user.ActivatedDate = _dateTime.Now;
        }

        else
            user.FirstLogin = false;

        await _context.SaveChangesAsync(cancellationToken);

        if (sendMessage)
        {
            await _mediator.Send(new SendPaymentDoneMessageCommand() { ClientOrderId = clientOrders.First().Id }, cancellationToken);
        }
        var role = await _userManager.GetRoleAsync(user);
        var token = _jwtAuthManager.GenerateTokens(request.PhoneNumber, role, user.Id.ToString(),
            user.FirstLogin.ToString());


        var clientOrder = await _context
            .ClientOrders
            .Where(w => w.Active == 1 && w.ClientOrderStatus == ClientOrderStatuses.New && w.ClientId == user.Id)
            .Include(i => i.ClientOrderDetails)
            .FirstOrDefaultAsync(cancellationToken);

        if (clientOrder != null)
        {
            clientOrder.CreatedBy = request.PhoneNumber;
            clientOrder.ClientId = user.Id;
            clientOrder.DeviceId = null;

            foreach (var clientOrderDetail in clientOrder.ClientOrderDetails)
            {
                clientOrderDetail.CreatedBy = request.PhoneNumber;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        return new LoginResult
        {
            UserName = request.PhoneNumber,
            Role = role,
            PopupShowingDate = user.PopupShowingDate,
            AccessToken = token.AccessToken,
            RefreshToken = token.RefreshToken.TokenString,
            ClientOrders = guestOrders
        };
    }
}
