using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.User.Commands.Login.V2;

public class Handler : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTime _dateTime;
    private readonly IConfiguration _configuration;
    private readonly IJwtAuthManager _jwtAuthManager;
    private readonly IUserManager _userManager;

    public Handler(
        IUserManager userManager
        , IJwtAuthManager jwtAuthManager
        , IApplicationDbContext context
        , IDateTime dateTime
        , IConfiguration configuration
    )
    {
        _userManager = userManager;
        _jwtAuthManager = jwtAuthManager;
        _context = context;
        _dateTime = dateTime;
        _configuration = configuration;
    }

    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        //var user = await _userManager.FindByPhoneAsync(request.PhoneNumber);
        var user = await _context.ApplicationUsers.FirstOrDefaultAsync(x =>
            x.PhoneNumber == request.PhoneNumber && x.Active == true, cancellationToken);

        if (user == null)

            throw new AppNotFoundException("User not found");

        if (user.LockoutEnd != null) throw new AppBadRequestException("Account Locked");

        var verifyStatus = false;

        if (bool.Parse(_configuration["development"]) &&
            (user.UserName is "00966700000000" or "00966700000001" or "00966700000002"))
        {
            verifyStatus = true;
        }

        else
        {
            verifyStatus = await _userManager.VerifyTwoFactorFourDigitsTokenAsync(
                user, request.Token);
        }

        if (!verifyStatus) throw new AppBadRequestException("Invalid Token");
        if (!user.PhoneNumberConfirmed) user.PhoneNumberConfirmed = true;
        // await _context.SaveChangesAsync(cancellationToken);
        if (user.FirstLogin == null)
        {
            user.FirstLogin = true;
            user.ActivatedDate = _dateTime.Now;
        }

        else
            user.FirstLogin = false;

        await _context.SaveChangesAsync(cancellationToken);

        var role = await _userManager.GetRoleAsync(user);
        var token = _jwtAuthManager.GenerateTokens(request.PhoneNumber, role, user.Id.ToString(),
            user.FirstLogin.ToString());


        var clientOrder = await _context
            .ClientOrders
            .Where(w => w.DeviceId == request.DeviceId && w.Active == 1 && w.ClientOrderStatus == ClientOrderStatuses.New && w.ClientId == user.Id)
            .Include(i => i.ClientOrderDetails)
            .SingleOrDefaultAsync(cancellationToken);

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
            AccessToken = token.AccessToken,
            RefreshToken = token.RefreshToken.TokenString
        };
    }
}
