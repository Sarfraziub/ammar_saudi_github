using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Notifications.Models;
using AutoMapper;
using Domain.Model.Message;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WhatsAppMessaging.Command.SendGiftMessage
{
    public class SendGiftMessageCommand : IRequest<Unit>
    {
        public long ClientOrderId { get; set; }
    }

    public class Validator : AbstractValidator<SendGiftMessageCommand>
    {
        public Validator()
        {
            RuleFor(e => e.ClientOrderId)
                .NotNull()
                .NotEmpty();
        }
    }
    public class SendGiftMessageCommandHandler : IRequestHandler<SendGiftMessageCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWhatsAppService _whatsAppService;
        private readonly ICurrentUserService _currentUserService;

        public SendGiftMessageCommandHandler(IApplicationDbContext context,
            IMapper mapper,
            IWhatsAppService whatsAppService,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _whatsAppService = whatsAppService;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(SendGiftMessageCommand request, CancellationToken cancellationToken)
        {
            var gift = await _context.Gifts.FirstOrDefaultAsync(
                x => x.ClientOrderId == request.ClientOrderId && x.Active == 1, cancellationToken);
            if(gift == null) return Unit.Value;
            var contentSettings =
                await _context.ContentSettings.FirstOrDefaultAsync(x => x.Key == "WhatsApp.SendGiftMessage",
                    cancellationToken);

            var sendGiftModel = new SendGiftModel()
            {
                SenderName = gift?.SenderName,
                ReceiverName = gift?.ReceiverName
            };

            var body = _whatsAppService.GetEmailBody(sendGiftModel, contentSettings?.ArabicContent);

            Task.Run(() =>
            {
                _whatsAppService.SendMessageAsync(new SMSMessageDto()
                {
                    To = gift.PhoneNumber,
                    Content = body,
                    Type = "media",
                    FileUrl = "https://down.qatrat-app.net/image/gift_image.jpg"
                });
            });

            return Unit.Value;
        }
    }
}
