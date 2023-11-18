using Application.Features.Common.Interfaces;
using Application.Location.Commands.AddLocationImage;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Location.Commands.RemoveLocationImage;

public class Handler : IRequestHandler<RemoveLocationImageCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(RemoveLocationImageCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.LocationImages.FindAsync(request.Id);
		entity.Active = 0;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}
