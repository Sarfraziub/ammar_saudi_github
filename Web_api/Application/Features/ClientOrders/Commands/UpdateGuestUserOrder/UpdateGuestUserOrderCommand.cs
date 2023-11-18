using Application.Features.ClientOrders.Commands.AddClientOrderLog;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Notifications.Models;
using Application.Interface;
using Domain.DbModel;
using Domain.Model.Message;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Commands.UpdateGuestUserOrder
{
    public class UpdateGuestUserOrderCommand : IRequest<Unit>
    {
        public List<long> OrderIds { get; set; }
        public bool ShouldTransferOrders { get; set; }
        public string GuestToken { get; set; }
    }

    public class Handler : IRequestHandler<UpdateGuestUserOrderCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        private readonly IWhatsAppService _whatsAppService;

        public Handler(IApplicationDbContext context, IMediator mediator, ICurrentUserService currentUserService, IWhatsAppService whatsAppService)
        {
            _context = context;
            _mediator = mediator;
            _currentUserService = currentUserService;
            _whatsAppService = whatsAppService;
        }

        public async Task<Unit> Handle(UpdateGuestUserOrderCommand request, CancellationToken cancellationToken)
        {
            var messageList = new List<SMSMessageDto>();
            
            if (request.ShouldTransferOrders)
            {
                var clientOrders = await _context.ClientOrders
                    .Include(x=> x.Client)
                    .Where(x=> request.OrderIds.Contains(x.Id))
                    .ToListAsync(cancellationToken);

                if (!clientOrders.Any()) return Unit.Value;

                var guestClientId = clientOrders.First().ClientId;
                foreach (var clientOrder in clientOrders)
                {
                    clientOrder.ClientId = _currentUserService.UserId;
                }

                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == guestClientId, cancellationToken);
                user.Active = false;

                await _context.SaveChangesAsync(cancellationToken);

                clientOrders = await _context.ClientOrders
                    .Include(x => x.Client)
                    .Where(x => request.OrderIds.Contains(x.Id))
                    .ToListAsync(cancellationToken);

                var paymentReceiveOrders = clientOrders
                    .Where(x => x.ClientOrderStatus == ClientOrderStatuses.PaymentReceived).ToList();

                var contentSettings =
                    await _context.ContentSettings.FirstOrDefaultAsync(x => x.Key == "WhatsApp.PaymentDoneMessage",
                        cancellationToken);

                //var keys = new List<string>
                //{
                //    "WhatsAppSettings.InstanceId",
                //    "WhatsAppSettings.AccessToken"
                //};
                //var settings = (await _context.Settings.Where(x => keys.Contains(x.Key)).ToListAsync(cancellationToken))
                //    .ToDictionary(x => x.Key.Split("WhatsAppSettings.")[1], x => x.Value);

                foreach (var clientOrder in paymentReceiveOrders)
                {
                    var paymentDoneModel = new PaymentDoneModel()
                    {
                        Id = clientOrder.Id.ToString(),
                        OrderNumber = clientOrder.Number,
                        PaymentDate = clientOrder.PaymentDate?.ToString("dddd, dd MMMM yyyy")
                    };

                    var body = GetEmailBody(paymentDoneModel, contentSettings.ArabicContent);

                    messageList.Add(new SMSMessageDto()
                    {
                        To = clientOrder.Client.PhoneNumber,
                        Content = body
                    });
                }
                Task.Run(() =>
                {
                    _whatsAppService.SendMessageAsync(messageList);
                });
            }
            return Unit.Value;
        }

        private string GetEmailBody<T>(T emailBodyModel, string body)
        {
            var dict = emailBodyModel.GetType().GetProperties().ToDictionary(property => property.Name,
                property => property.GetValue(emailBodyModel));

            foreach (var key in dict.Keys)
            {
                body = body.Replace($"<<{key}>>", (string?)dict[key]);
            };

            return body;
        }
    }
}
