using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.RatesAndFeedbacks.Commands.ShowHideFeedback;

public class Handler : IRequestHandler<ShowHideFeedbackCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(ShowHideFeedbackCommand request, CancellationToken cancellationToken)
	{
		var clientOrder = await _context.ClientOrders
			.SingleOrDefaultAsync(w => w.Id == request.ClientOrderId,
				cancellationToken);
		if (clientOrder == null) throw new AppNotFoundException("not found");
		clientOrder.HideFeedback = request.HideFeedback;
		// _context.ClientOrders.Add(clientOrder);
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


