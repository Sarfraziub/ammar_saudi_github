using Application.Features.Common.Interfaces;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.Regions.Commands.AddRegion;

public class Handler : IRequestHandler<AddRegionCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(AddRegionCommand request, CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<Region>(request);
		// entity.TeamCount = 0;
		_context.Regions.Add(entity);
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


