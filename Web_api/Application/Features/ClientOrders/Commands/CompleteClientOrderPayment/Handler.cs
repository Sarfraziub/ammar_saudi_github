using Application.Features.ClientOrders.Commands.AddClientOrderLog;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Firebase;
using Application.Features.Common.Models.Notifications.Models;
using Application.Features.WhatsAppMessaging.Command.SendGiftMessage;
using Application.Features.WhatsAppMessaging.Command.SendPaymentDoneMessage;
using Domain;
using Domain.DbModel;
using Domain.Model.Message;
using EnumStringValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.ClientOrderPayments.Commands.ReceiveClientOrderPaymentPayload.Handler;

namespace Application.Features.ClientOrders.Commands.CompleteClientOrderPayment;

public class Handler : IRequestHandler<CompleteClientOrderPaymentCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IFirebaseService _firebaseService;
    private readonly IMediator _mediator;
    private readonly IUserManager _userManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;
    private readonly IWhatsAppService _whatsAppService;


    public Handler(IApplicationDbContext context, 
        IMediator mediator, 
        IFirebaseService firebaseService, 
        IUserManager userManager, 
        ICurrentUserService currentUserService, 
        IDateTime dateTime,
        IWhatsAppService whatsAppService)
    {
        _context = context;
        _mediator = mediator;
        _firebaseService = firebaseService;
        _userManager = userManager;
        _currentUserService = currentUserService;
        _dateTime = dateTime;
        _whatsAppService = whatsAppService;
    }

    public async Task<Unit> Handle(CompleteClientOrderPaymentCommand request, CancellationToken cancellationToken)
    {
        var paymentTry = await _context.PaymentTries.FindAsync(request.PaymentTryId);
        if (paymentTry == null) return Unit.Value;

        var clientOrder = await _context.ClientOrders
            .Where(x => x.Id == paymentTry.ClientOrderId)
            .Include(i => i.ClientOrderDetails)
            .Include(x=> x.Client)
            .SingleAsync(cancellationToken);

        clientOrder.ClientOrderStatus = ClientOrderStatuses.PaymentReceived;
        clientOrder.PaymentDate = DateTime.UtcNow;
        clientOrder.DeviceSource = "WEB";
        clientOrder.PaymentDate = _dateTime.Now;
        await _context.SaveChangesAsync(cancellationToken);

        var firebaseMessage = new FirebaseMessage()
        {
            Title = "طلب جديد",
            Body = $"رقم الطلب: {clientOrder.Number}",
            Data = new Dictionary<string, string>()
            {
                { "locationUrl", clientOrder.Location != null ? clientOrder.Location.Url : null },
                { "clientOrderId", clientOrder.Id.ToString() },
            },
        };

        //Send Notifications for drivers
        if (clientOrder.Location != null)
        {
            await _firebaseService.SendDriversTopic(firebaseMessage);
        }

        var users = await _userManager.GetUsersInRoleAsync(ApplicationRoles.Driver.GetStringValue());
        foreach (var user in users)
        {
            _context.UserNotifications.Add(new UserNotification()
            {
                UserId = user.Id,
                Title = firebaseMessage.Title,
                Body = firebaseMessage.Body,
                ArabicTitle = firebaseMessage.ArabicTitle,
                ArabicBody = firebaseMessage.ArabicBody
            });
        }

        await _context.SaveChangesAsync(cancellationToken);


        var userRoles = await _userManager.GetRolesAsync(clientOrder.Client);

        if (!userRoles.Contains(ApplicationRoles.Guest.ToString()))
        {
            await _mediator.Send(new SendPaymentDoneMessageCommand() { ClientOrderId = clientOrder.Id });

            //var contentSettings =
            //    await _context.ContentSettings.FirstOrDefaultAsync(x => x.Key == "WhatsApp.PaymentDoneMessage",
            //        cancellationToken);

            //var paymentDoneModel = new PaymentDoneModel()
            //{
            //    Id = clientOrder.Id.ToString(),
            //    OrderNumber = clientOrder.Number,
            //    PaymentDate = clientOrder.PaymentDate?.ToString("dddd, dd MMMM yyyy")
            //};

            //var body = _whatsAppService.GetEmailBody(paymentDoneModel, contentSettings.ArabicContent);

            //var messageDto = new SMSMessageDto()
            //{
            //    To = clientOrder.Client.PhoneNumber,
            //    Content = body
            //};
            ////var keys = new List<string>
            ////{
            ////    "WhatsAppSettings.InstanceId",
            ////    "WhatsAppSettings.AccessToken"
            ////};
            ////var settings = (await _context.Settings.Where(x => keys.Contains(x.Key)).ToListAsync(cancellationToken))
            ////    .ToDictionary(x => x.Key.Split("WhatsAppSettings.")[1], x => x.Value);

            //Task.Run(() =>
            //{
            //    _whatsAppService.SendMessageAsync(messageDto);
            //});
        }
        await _mediator.Send(new SendGiftMessageCommand() { ClientOrderId = clientOrder.Id }, cancellationToken);

        if (_currentUserService.UserId != null)
        {
            var userDeviceToken = await _context
                .UserDeviceTokens
                .Where(w => w.UserId == _currentUserService.UserId)
                .OrderByDescending(o => o.Created)
                .Take(1)
                .SingleAsync(cancellationToken);
            await _firebaseService.SendUser(firebaseMessage, userDeviceToken.RegistrationToken);

            _context.UserNotifications.Add(new UserNotification()
            {
                UserId = _currentUserService.UserId.Value,
                Title = firebaseMessage.Title,
                Body = firebaseMessage.Body,
                ArabicTitle = firebaseMessage.ArabicTitle,
                ArabicBody = firebaseMessage.ArabicBody
            });
            await _context.SaveChangesAsync(cancellationToken);
        }

        await _mediator.Send(
            new AddClientOrderLogCommand
            {
                ClientOrderId = paymentTry.ClientOrderId,
                Description = "",
                ClientOrderActionLogStatus = ClientOrderActionLogStatuses.OrderPaid
            }, cancellationToken);

        return Unit.Value;
    }

    //private string GetEmailBody<T>(T emailBodyModel, string body)
    //{
    //    var dict = emailBodyModel.GetType().GetProperties().ToDictionary(property => property.Name,
    //        property => property.GetValue(emailBodyModel));

    //    foreach (var key in dict.Keys)
    //    {
    //        body = body.Replace($"<<{key}>>", (string?)dict[key]);
    //    };

    //    return body;
    //}
}
