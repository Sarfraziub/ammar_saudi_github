using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Notifications.Models;
using AutoMapper;
using Domain.Model.Message;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WhatsAppMessaging.Command.SendPaymentDoneMessage
{
    public class SendPaymentDoneMessageCommand : IRequest<Unit>
    {
        public long ClientOrderId { get; set; }
    }

    public class Validator : AbstractValidator<SendPaymentDoneMessageCommand>
    {
        public Validator()
        {
            RuleFor(e => e.ClientOrderId)
                .NotNull()
                .NotEmpty();
        }
    }
    public class SendPaymentDoneMessageCommandHandler : IRequestHandler<SendPaymentDoneMessageCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWhatsAppService _whatsAppService;
        private readonly ICurrentUserService _currentUserService;

        public SendPaymentDoneMessageCommandHandler(IApplicationDbContext context,
            IMapper mapper,
            IWhatsAppService whatsAppService,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _whatsAppService = whatsAppService;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(SendPaymentDoneMessageCommand command, CancellationToken cancellationToken)
        {
            var clientOrder =
                await _context.ClientOrders.FirstOrDefaultAsync(x => x.Id == command.ClientOrderId && x.Active == 1,
                    cancellationToken);

            var contentSettings =
                await _context.ContentSettings.FirstOrDefaultAsync(x => x.Key == "WhatsApp.PaymentDoneMessage",
                    cancellationToken);
            var paymentDoneModel = new PaymentDoneModel()
            {
                Id = clientOrder?.Id.ToString(),
                OrderNumber = clientOrder?.Number,
                PaymentDate = clientOrder?.PaymentDate?.ToString("dddd, dd MMMM yyyy")
            };

            var body = _whatsAppService.GetEmailBody(paymentDoneModel, contentSettings?.ArabicContent);

            var messageDto = new SMSMessageDto()
            {
                To = clientOrder?.Client.PhoneNumber,
                Content = body
            };

            Task.Run(() =>
            {
                _whatsAppService.SendMessageAsync(messageDto);
            });

            return Unit.Value;
        }
    }
}
