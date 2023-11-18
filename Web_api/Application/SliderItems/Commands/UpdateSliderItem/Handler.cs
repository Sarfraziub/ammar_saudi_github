using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.SliderItems.Commands.UpdateSliderItem;

public class Handler : IRequestHandler<UpdateSliderItemCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(UpdateSliderItemCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.SliderItems.FindAsync(request.Id);
		entity.Name = request.Name;
		entity.ImageId = request.ImageId;
		entity.Order = request.Order;
		entity.Visible = request.Visible;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


