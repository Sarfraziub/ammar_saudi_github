using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserNotifications.Queries.GetUserNotifications;

public class Handler : IRequestHandler<GetUserNotificationsQuery, GetUserNotificationsVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;
	private readonly ICurrentUserService _currentUserService;

	public Handler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
	{
		_context = context;
		_mapper = mapper;
		_currentUserService = currentUserService;
	}

	public async Task<GetUserNotificationsVm> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
	{
		return new GetUserNotificationsVm
		{
			UserNotifications = await _context.UserNotifications
				.AsNoTracking()
				.Where(c => c.Active == 1 && c.UserId == _currentUserService.UserId)
				.ProjectTo<GetUserNotificationDto>(_mapper.ConfigurationProvider)
				.OrderByDescending(t => t.Created)
				.ToListAsync(cancellationToken)
		};
	}
}


