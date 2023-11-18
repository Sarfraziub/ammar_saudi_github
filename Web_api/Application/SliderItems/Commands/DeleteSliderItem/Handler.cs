using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.SliderItems.Commands.DeleteSliderItem;

public class Handler : IRequestHandler<DeleteSliderItemCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(DeleteSliderItemCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.SliderItems.FindAsync(request.Id);
		entity.Active = 0;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


