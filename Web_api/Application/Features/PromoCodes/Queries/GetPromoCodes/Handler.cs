using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PromoCodes.Queries.GetPromoCodes;

public class Handler : IRequestHandler<GetPromoCodesQuery, GetPromoCodesVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<GetPromoCodesVm> Handle(GetPromoCodesQuery request, CancellationToken cancellationToken)
	{
		return new GetPromoCodesVm
		{
			PromoCodes = await _context.PromoCodes
				.AsNoTracking()
				.Where(c => c.Active == 1)
				.ProjectTo<GetPromoCodeDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};
	}
}


