using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.NotificationTemplates.Queries.GetNotificationTemplates;

public class Handler : IRequestHandler<GetNotificationTemplatesQuery, GetNotificationTemplatesVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<GetNotificationTemplatesVm> Handle(GetNotificationTemplatesQuery request,
		CancellationToken cancellationToken)
	{
		return new GetNotificationTemplatesVm
		{
			NotificationTemplates = await _context.NotificationTemplates
				.AsNoTracking()
				.Where(c => c.Active == 1)
				.ProjectTo<GetNotificationTemplateDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};
	}
}


