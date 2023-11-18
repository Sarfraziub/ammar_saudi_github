using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Notifications.Models;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.User.Commands.SendPhoneCodeToken.V1;

public class Handler : IRequestHandler<SendPhoneCodeConfirmationCommand>
{
	private readonly IApplicationDbContext _context;
	private readonly ISMSService _smsService;
	private readonly IEmailService _emailService;
	private readonly IConfiguration _configuration;
	private readonly IUserManager _userManager;

	public Handler(
		IUserManager userManager
		, IApplicationDbContext context
		, ISMSService smsService
		, IEmailService emailService
		, IConfiguration configuration
	)
	{
		_userManager = userManager;

		_context = context;
		_smsService = smsService;
		_emailService = emailService;
		_configuration = configuration;
	}

	public async Task<Unit> Handle(SendPhoneCodeConfirmationCommand request, CancellationToken cancellationToken)
    {
        var token = request.Token ?? new Random().Next(1000, 9999).ToString();

        var response = await _smsService.SendSmsAsync(new SMSMessageDto
        {
            To = request.PhoneNumber,
            Content = $"Your Qatarat verification code is: {token}"
        });

        _context.Otps.Add(new Otp()
        {
            PhoneNumber = request.PhoneNumber,
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
