using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.RatesAndFeedbacks.Commands.SetRateAndFeedback;

public class Handler : IRequestHandler<SetRateAndFeedbackCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly ICurrentUserService _currentUserService;

	public Handler(IApplicationDbContext context, ICurrentUserService currentUserService)
	{
		_context = context;
		_currentUserService = currentUserService;
	}

	public async Task<Unit> Handle(SetRateAndFeedbackCommand request, CancellationToken cancellationToken)
	{
		var clientOrder = await _context.ClientOrders
			.SingleOrDefaultAsync(w => w.Id == request.ClientOrderId && w.ClientId == _currentUserService.UserId,
				cancellationToken);
		if (clientOrder == null) throw new AppNotFoundException("not found");
		clientOrder.Rate = request.Rate;
		clientOrder.Feedback = request.Feedback;
		// _context.ClientOrders.Add(clientOrder);
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


