using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.NotificationTemplates.Queries.GetNotificationTemplateById;

public class Handler : IRequestHandler<GetNotificationTemplateByIdQuery, GetNotificationTemplateByIdDto>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<GetNotificationTemplateByIdDto> Handle(GetNotificationTemplateByIdQuery request,
		CancellationToken cancellationToken)
	{
		return await _context.NotificationTemplates
				.AsNoTracking()
				.Where(c => c.Id == request.Id && c.Active == 1)
				.ProjectTo<GetNotificationTemplateByIdDto>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync(cancellationToken)
			;
	}
}


