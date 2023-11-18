using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Notifications.Models;
using Domain.DbModel;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.User.Commands.SendWhatsAppCodeToken
{
    public class SendWhatsAppCodeTokenCommand : IRequest
    {
        public string PhoneNumber { get; set; }
        public bool SendNew { get; set; } = true;
    }

    public class Validator : AbstractValidator<SendWhatsAppCodeTokenCommand>
    {
        public Validator()
        {
            RuleFor(v => v.PhoneNumber)
                .NotEmpty()
                .NotNull();
        }
    }


    public class SendWhatsAppCodeTokenCommandHandler : IRequestHandler<SendWhatsAppCodeTokenCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IWhatsAppService _whatsAppService;

        public SendWhatsAppCodeTokenCommandHandler(
              IUserManager userManager, 
              IApplicationDbContext context,
              IWhatsAppService whatsAppService
              )
        {
            _context = context;
            _whatsAppService = whatsAppService;
        }

        public async Task<Unit> Handle(SendWhatsAppCodeTokenCommand request, CancellationToken cancellationToken)
        {
            string newToken;
            var token = newToken = new Random().Next(1000, 9999).ToString();

            if(!request.SendNew)
            {
                token = (await _context.Otps
                    .Where(x => x.PhoneNumber == request.PhoneNumber)
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefaultAsync(cancellationToken))?.Code;
                token ??= newToken;
            }
            //var keys = new List<string>
            //{
            //    "WhatsAppSettings.InstanceId",
            //    "WhatsAppSettings.AccessToken"
            //};
            //var settings = (await _context.Settings.Where(x => keys.Contains(x.Key)).ToListAsync(cancellationToken))
            //    .ToDictionary(x => x.Key.Split("WhatsAppSettings.")[1], x => x.Value);

            //Task.Run(() =>
            //{
            //    _whatsAppService.SendMessageAsync(settings, new SMSMessageDto
            //    {
            //        To = request.PhoneNumber,
            //        Content = $"Your Qatarat verification code is: {token}"
            //    });
            //});

            //var response = await _whatsAppService.SendMessageAsync(new SMSMessageDto
            //{
            //    To = request.PhoneNumber,
            //    Content = $"Your Qatarat verification code is: {token}"
            //});

            _context.Otps.Add(new Otp()
            {
                PhoneNumber = request.PhoneNumber,
                Code = token,
                OtpResponse = string.Empty
            });
            await _context.SaveChangesAsync(cancellationToken);

            Task.Run(() =>
            {
                _whatsAppService.SendMessageAsync(new SMSMessageDto()
                {
                    To = request.PhoneNumber,
                    Content = $"Your Qatarat verification code is: {token}"
                });
            });


            return Unit.Value;
        }
    }
}
