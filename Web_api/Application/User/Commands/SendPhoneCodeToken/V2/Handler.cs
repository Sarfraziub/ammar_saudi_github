using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Notifications.Models;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.User.Commands.SendPhoneCodeToken.V2;

public class Handler : IRequestHandler<SendPhoneCodeConfirmationCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ISMSService _smsService;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;
    private readonly ITokenStoreService _tokenStoreService;
    private readonly IUserManager _userManager;

    public Handler(
        IUserManager userManager
        , IApplicationDbContext context
        , ISMSService smsService
        , IEmailService emailService
        , IConfiguration configuration
        , ITokenStoreService tokenStoreService
    )
    {
        _userManager = userManager;

        _context = context;
        _smsService = smsService;
        _emailService = emailService;
        _configuration = configuration;
        _tokenStoreService = tokenStoreService;
    }

    public async Task<Unit> Handle(SendPhoneCodeConfirmationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByPhoneAsync(request.PhoneNumber);


        if (user == null) throw new AppNotFoundException(nameof(ApplicationUser), request.PhoneNumber);
        if (user.LockoutEnd != null) throw new AppBadRequestException("Account Locked");
        var token = _tokenStoreService.GetToken(user.Id.ToString());
        if (string.IsNullOrEmpty(token))
        {
            token = await _userManager.GenerateUserFourDigitsTokenAsync(user);
        }


        var response = await _smsService.SendSmsAsync(new SMSMessageDto
        {
            To = user.PhoneNumber,
            Content = $"Code: {token}"
        });

        _context.Otps.Add(new Otp()
        {
            UserId = user.Id,
            Code = token,
            OtpResponse = response
        });
        await _context.SaveChangesAsync(cancellationToken);


        if (bool.Parse(_configuration["development"]))
        {
            await _emailService.SendEmailToDev("Otp", token);
        }


        return Unit.Value;
    }
}
