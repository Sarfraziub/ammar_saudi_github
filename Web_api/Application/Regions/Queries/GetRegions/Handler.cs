using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Regions.Queries.GetRegions;

public class Handler : IRequestHandler<GetRegionsQuery, RegionsVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<RegionsVm> Handle(GetRegionsQuery request, CancellationToken cancellationToken)
	{
		return new RegionsVm
		{
			Regions = await _context.Regions
				.AsNoTracking()
				.Where(c => c.Active == 1)
				.ProjectTo<GetRegionDto>(_mapper.ConfigurationProvider)
				//.OrderBy(t => t.Title)
				.ToListAsync(cancellationToken)
		};
	}
}


