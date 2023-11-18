using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.DriverFees.Queries.GetDriverFeeSettings;

public class Handler : IRequestHandler<GetDriverFeeSettingsQuery, List<GetDriverFeeSettingsDto>>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<List<GetDriverFeeSettingsDto>> Handle(GetDriverFeeSettingsQuery request, CancellationToken cancellationToken)
	{
		return  await _context.DriverFees
				.AsNoTracking()
				.Where(c => c.Active == 1)
                .OrderBy(c=> c.IsOffer)
				.ProjectTo<GetDriverFeeSettingsDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);
	}
}


