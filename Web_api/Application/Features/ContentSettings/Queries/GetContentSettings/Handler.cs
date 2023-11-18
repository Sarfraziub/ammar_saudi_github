using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ContentSettings.Queries.GetContentSettings;

public class Handler : IRequestHandler<GetContentSettingsQuery, GetContentSettingsDto>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<GetContentSettingsDto> Handle(GetContentSettingsQuery request, CancellationToken cancellationToken)
	{
		return await _context.ContentSettings
				.AsNoTracking()
				.Where(c => c.Active == 1 && c.Key == "Social.Content")
				.ProjectTo<GetContentSettingsDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(cancellationToken)
			;
	}
}


