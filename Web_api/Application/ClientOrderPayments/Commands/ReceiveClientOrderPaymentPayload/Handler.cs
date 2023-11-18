using System.Text.Json;
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

namespace Application.ClientOrderPayments.Commands.ReceiveClientOrderPaymentPayload;

public class Handler : IRequestHandler<ReceiveClientOrderPaymentPayloadCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly ICurrentUserService _currentUserService;
	private readonly IMediator _mediator;
	private readonly IFirebaseService _firebaseService;
	private readonly IUserManager _userManager;
    private readonly IDateTime _dateTime;
    private readonly IWhatsAppService _whatsAppService;

    public Handler(
		IApplicationDbContext context
		, ICurrentUserService currentUserService
		, IMediator mediator
		, IFirebaseService firebaseService
		, IUserManager userManager, 
        IDateTime dateTime, 
        IWhatsAppService whatsAppService)
	{
		_context = context;
		_currentUserService = currentUserService;
		_mediator = mediator;
		_firebaseService = firebaseService;
		_userManager = userManager;
        _dateTime = dateTime;
        _whatsAppService = whatsAppService;
    }

	public async Task<Unit> Handle(ReceiveClientOrderPaymentPayloadCommand request, CancellationToken cancellationToken)
    {
        var cartId = (string)request.PaymentResponse["cart_id"];
        var entity = await _context.ClientOrderPayments.FirstOrDefaultAsync(x => x.CartId == cartId, cancellationToken: cancellationToken);
        entity.PaymentResponse = JsonSerializer.Serialize(request.PaymentResponse);
        entity.TransactionReference = (string)request.PaymentResponse["tran_ref"];

		var clientOrder = await _context.ClientOrders
			.Where(x => x.Id == entity.ClientOrderId)
			.Include(i => i.ClientOrderDetails)
            .Include(x=> x.Client)
            .SingleAsync(cancellationToken);

		clientOrder.ClientOrderStatus = ClientOrderStatuses.PaymentReceived;
        clientOrder.DeviceSource = "MOBILE";
        clientOrder.DeviceToken = request.DeviceToken;
        clientOrder.PaymentDate = _dateTime.Now;
        await _context.SaveChangesAsync(cancellationToken);

		var firebaseMessage = new FirebaseMessage()
		{
			// Title = "New Order",
			// Body = $"Order Number: {clientOrder.Number}",
			Title = "طلب جديد",
			Body = $"رقم الطلب: {clientOrder.Number}",
			Data = new Dictionary<string, string>()
			{
				{ "locationUrl", clientOrder.Location != null ? clientOrder.Location.Url : null },
				{ "clientOrderId", clientOrder.Id.ToString() },
			},
		};

        var userRoles = await _userManager.GetRolesAsync(clientOrder.Client);

        if (!userRoles.Contains(ApplicationRoles.Guest.ToString()))
        {
            //var contentSettings =
            //    await _context.ContentSettings.FirstOrDefaultAsync(x => x.Key == "WhatsApp.PaymentDoneMessage",
            //        cancellationToken);
            //var paymentDoneModel = new PaymentDoneModel()
            //{
            //    Id = clientOrder.Id.ToString(),
            //    OrderNumber = clientOrder.Number,
            //    PaymentDate = clientOrder.PaymentDate?.ToString("dddd, dd MMMM yyyy")
            //};

            //var body = GetEmailBody(paymentDoneModel, contentSettings.ArabicContent);

            //var messageDto = new SMSMessageDto()
            //{
            //    To = clientOrder.Client.PhoneNumber,
            //    Content = body
            //};

            //Task.Run(() =>
            //{
            //    _whatsAppService.SendMessageAsync(messageDto);
            //});
            await _mediator.Send(new SendPaymentDoneMessageCommand() { ClientOrderId = clientOrder.Id });

        }
        await _mediator.Send(new SendGiftMessageCommand() { ClientOrderId = clientOrder.Id }, cancellationToken);

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

		var userDeviceToken = await _context
			.UserDeviceTokens
			.Where(w=>w.UserId == clientOrder.ClientId)
			.OrderByDescending(o => o.Created)
			.Take(1)
			.SingleAsync(cancellationToken);
		await _firebaseService.SendUser(firebaseMessage, userDeviceToken.RegistrationToken);

		_context.UserNotifications.Add(new UserNotification()
		{
			UserId = clientOrder.ClientId.Value,
			Title = firebaseMessage.Title,
			Body = firebaseMessage.Body,
			ArabicTitle = firebaseMessage.ArabicTitle,
			ArabicBody = firebaseMessage.ArabicBody
		});
		await _context.SaveChangesAsync(cancellationToken);

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
