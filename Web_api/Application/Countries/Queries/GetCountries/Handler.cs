using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Countries.Queries.GetCountries;

public class Handler : IRequestHandler<GetCountriesQuery, GetCountriesVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<GetCountriesVm> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
	{
		return new GetCountriesVm
		{
			Countries = await _context.Countries
				.AsNoTracking()
				.Where(c => c.Active == 1)
				.ProjectTo<GetCountryDto>(_mapper.ConfigurationProvider)
				.OrderBy(t => t.Id)
				.ToListAsync(cancellationToken)
		};
	}
}

