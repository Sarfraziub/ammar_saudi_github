using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PromoCodes.Queries.GetPromoCodeById;

public class Handler : IRequestHandler<GetPromoCodeByIdQuery, GetPromoCodeByIdDto>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<GetPromoCodeByIdDto> Handle(GetPromoCodeByIdQuery request, CancellationToken cancellationToken)
	{
		return await _context.PromoCodes
				.AsNoTracking()
				//.Include(i => i.HarvestType)
				.Where(c => c.Id == request.Id && c.Active == 1)
				.ProjectTo<GetPromoCodeByIdDto>(_mapper.ConfigurationProvider)
				//.FindAsync(request.Id)
				.SingleOrDefaultAsync(cancellationToken)
			;
	}
}


