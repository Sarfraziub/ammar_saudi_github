using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Commands.CancelPromoCode;

public class Handler : IRequestHandler<CancelPromoCodeCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly ICurrentUserService _currentUserService;
	private readonly IErrorMessagesService _errorMessagesService;

	public Handler(IApplicationDbContext context, ICurrentUserService currentUserService,
		IErrorMessagesService errorMessagesService)
	{
		_context = context;
		_currentUserService = currentUserService;
		_errorMessagesService = errorMessagesService;
	}

	public async Task<Unit> Handle(CancelPromoCodeCommand request, CancellationToken cancellationToken)
	{
		ClientOrder clientOrder;
		if (_currentUserService.UserId != null)
			clientOrder = await
				_context.ClientOrders.SingleOrDefaultAsync(s =>
					s.ClientOrderStatus == ClientOrderStatuses.New
					&& s.Active == 1
					&& s.ClientId == _currentUserService.UserId);
		else
			clientOrder = await
				_context.ClientOrders.SingleOrDefaultAsync(s =>
					s.ClientOrderStatus == ClientOrderStatuses.New
					&& s.Active == 1
					&& s.DeviceId == request.DeviceId);

		if (clientOrder == null) throw new AppBadRequestException(_errorMessagesService.GetCommonErrorMessageById(4));


		clientOrder.PromoCodeId = null;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}
