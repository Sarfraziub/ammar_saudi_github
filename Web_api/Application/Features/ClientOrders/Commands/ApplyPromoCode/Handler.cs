using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Domain.DbModel;
using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Commands.ApplyPromoCode;

public class Handler : IRequestHandler<ApplyPromoCodeCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly ICurrentUserService _currentUserService;
	private readonly IErrorMessagesService _errorMessagesService;
    //private static readonly string[] DEVICE_SOURCES = new string[] { "Android", "webOS", "iPhone", "iPad", "iPod", "BlackBerry", "Windows Phone"};
    private static readonly string[] DEVICE_SOURCES = new string[] { "Mozilla","Firefox", "Chrome", "Safari"};

    public Handler(IApplicationDbContext context, ICurrentUserService currentUserService,
		IErrorMessagesService errorMessagesService)
	{
		_context = context;
		_currentUserService = currentUserService;
		_errorMessagesService = errorMessagesService;
	}

	public async Task<Unit> Handle(ApplyPromoCodeCommand request, CancellationToken cancellationToken)
	{
		var promoCode =
			await _context.PromoCodes.SingleOrDefaultAsync(s
					=> s.Code.ToLower() == request.PromoCode.ToLower()
					   && s.Active == 1
				,
				cancellationToken);
		if (promoCode == null) 
            throw new AppBadRequestException(_errorMessagesService.GetOrderErrorMessageById(1));
		
		if (promoCode.Expiry < DateTime.Now)
			throw new AppBadRequestException(_errorMessagesService.GetOrderErrorMessageById(1));

		ClientOrder clientOrder;
		if (_currentUserService.UserId != null)
			clientOrder = await
				_context.ClientOrders.SingleAsync(s =>
					s.ClientOrderStatus == ClientOrderStatuses.New && s.ClientId == _currentUserService.UserId &&
					s.Active == 1);
		else
			clientOrder = await
				_context.ClientOrders.SingleAsync(s =>
					s.ClientOrderStatus == ClientOrderStatuses.New && s.DeviceId == request.DeviceId &&
					s.Active == 1);

		clientOrder.PromoCodeId = promoCode.Id;

        //if (request.UserAgent != null)
        //{
        //    foreach (var device in DEVICE_SOURCES)
        //    {
        //        if (request.UserAgent.Contains(device))
        //        {
        //            clientOrder.PromoCodeAppliedSource = "Mobile";
        //            break;
        //        }
        //        clientOrder.PromoCodeAppliedSource = "Web";
        //    }
        //}

        //if (request.DeviceId != null)
        //{
        //    foreach (var device in DEVICE_SOURCES)
        //    {
        //        if (request.DeviceId.Contains(device))
        //        {
        //            clientOrder.PromoCodeAppliedSource = "Web";
        //            break;
        //        }
        //        clientOrder.PromoCodeAppliedSource = "Mobile";
        //    }
        //}
        clientOrder.PromoCodeAppliedSource = request.UserAgent;

        if (promoCode.ApplicableType != (int)PromoCodeApplicableType.WebAndMobile)
        {
            switch (promoCode.ApplicableType)
            {
                case (int)PromoCodeApplicableType.Web when clientOrder.PromoCodeAppliedSource != "web":
                    throw new Exception("Promo code is only applicable from web");
                case (int)PromoCodeApplicableType.Mobile when clientOrder.PromoCodeAppliedSource != "mobile":
                    throw new Exception("Promo code is only applicable from mobile");
                default:
                    await _context.SaveChangesAsync(cancellationToken);
                    break;
            }
        }
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
	}
}
