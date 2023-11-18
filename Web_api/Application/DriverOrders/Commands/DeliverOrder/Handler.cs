using Application.Features.ClientOrders.Commands.AddClientOrderLog;
using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Firebase;
using Application.Features.Common.Models.Notifications.Models;
using Domain;
using Domain.DbModel;
using Domain.Model.Message;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverOrders.Commands.DeliverOrder;

public class Handler : IRequestHandler<DeliverOrderCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly IErrorMessagesService _errorMessagesService;
	private readonly IMediator _mediator;
	private readonly IDateTime _dateTime;
    private readonly IFirebaseService _firebaseService;
    private readonly IUserManager _userManager;
    private readonly IWhatsAppService _whatsAppService;

    public Handler(
        IApplicationDbContext context,
		IErrorMessagesService errorMessagesService, 
        IMediator mediator,
        IDateTime dateTime, 
        IFirebaseService firebaseService, 
        IUserManager userManager, 
        IWhatsAppService whatsAppService)
	{
		_context = context;
		_errorMessagesService = errorMessagesService;
		_mediator = mediator;
		_dateTime = dateTime;
        _firebaseService = firebaseService;
        _userManager = userManager;
        _whatsAppService = whatsAppService;
    }

	public async Task<Unit> Handle(DeliverOrderCommand request, CancellationToken cancellationToken)
	{
		var clientOrder = await
			_context.ClientOrders.
                Include(x=> x.Client)
                .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

		if (clientOrder == null)
			throw new AppBadRequestException(_errorMessagesService.GetCommonErrorMessageById(4));


		clientOrder.ClientOrderStatus = ClientOrderStatuses.Delivered;
		clientOrder.DeliveryTime = _dateTime.Now;

        if (!string.IsNullOrEmpty(clientOrder.DeviceToken))
        {
			var firebaseMessage = new FirebaseMessage()
			{
                Title = "تطبيق قطرات",
				Body = $"تم توصيل الطلب ، نأمل تقييم الخدمة وإبداء رأيك ونتطلع للتعامل معك مجدداََ\r\nشكراََ لاختيارك قطرات 🌹",
				Data = new Dictionary<string, string>()
				{
					{ "locationUrl", clientOrder.Location != null ? clientOrder.Location.Url : null },
					{ "clientOrderId", clientOrder.Id.ToString() },
				},
			};
            await _firebaseService.SendUser(firebaseMessage, clientOrder.DeviceToken);
        }
        var userRoles = await _userManager.GetRolesAsync(clientOrder.Client);

        if (!userRoles.Contains(ApplicationRoles.Guest.ToString()))
        {
            var contentSettings =
                await _context.ContentSettings.FirstOrDefaultAsync(x => x.Key == "WhatsApp.DeliveryDoneMessage",
                    cancellationToken);
            var deliveryDoneModel = new DeliveryDoneModel()
            {
                Id = clientOrder.Id.ToString(),
                OrderNumber = clientOrder.Number,
                PaymentDate = clientOrder.DeliveryTime?.ToString("dddd, dd MMMM yyyy")
            };

            var body = GetEmailBody(deliveryDoneModel, contentSettings.ArabicContent);

            var messageDto = new SMSMessageDto()
            {
                To = clientOrder.Client.PhoneNumber,
                Content = body
            };

            var keys = new List<string>
            {
                "WhatsAppSettings.InstanceId",
                "WhatsAppSettings.AccessToken"
            };
            var settings = (await _context.Settings.Where(x => keys.Contains(x.Key)).ToListAsync(cancellationToken))
                .ToDictionary(x => x.Key.Split("WhatsAppSettings.")[1], x => x.Value);

            Task.Run(() =>
            {
                _whatsAppService.SendMessageAsync(settings, messageDto);
            });
        }

        foreach (var image in request.Images)
		{
			_context.ClientOrderDeliverImages.Add(entity: new ClientOrderDeliverImage()
			{
				ClientOrderId = request.Id, FileId = image
			});
		}

		await _context.SaveChangesAsync(cancellationToken);

        await _mediator.Send(
			new AddClientOrderLogCommand
			{
				ClientOrderId = clientOrder.Id, Description = "",
				ClientOrderActionLogStatus = ClientOrderActionLogStatuses.OrderDelivered
			}, cancellationToken);


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
