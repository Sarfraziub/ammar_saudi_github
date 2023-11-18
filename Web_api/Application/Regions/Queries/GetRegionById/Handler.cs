using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Regions.Queries.GetRegionById;

public class Handler : IRequestHandler<GetRegionByIdQuery, GetRegionByIdDto>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<GetRegionByIdDto> Handle(GetRegionByIdQuery request, CancellationToken cancellationToken)
	{
		return await _context.Regions
				.AsNoTracking()
				//.Include(i => i.HarvestType)
				.Where(c => c.Id == request.Id && c.Active == 1)
				.ProjectTo<GetRegionByIdDto>(_mapper.ConfigurationProvider)
				//.FindAsync(request.Id)
				.SingleOrDefaultAsync(cancellationToken)
			;
	}
}


