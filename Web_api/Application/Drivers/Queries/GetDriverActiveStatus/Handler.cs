using Application.Features.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Drivers.Queries.GetDriverActiveStatus;

public class Handler : IRequestHandler<GetDriverActiveStatusQuery, bool>
{
	private readonly IApplicationDbContext _context;
	private readonly ICurrentUserService _currentUserService;

	public Handler(IApplicationDbContext context, ICurrentUserService currentUserService)
	{
		_context = context;
		_currentUserService = currentUserService;
	}

	public async Task<bool> Handle(GetDriverActiveStatusQuery request, CancellationToken cancellationToken)
	{
		var entity = await _context.ApplicationUsers
				.AsNoTracking()
				.Where(c => c.Id == _currentUserService.UserId)
				.SingleAsync(cancellationToken)
			;

		return entity.ActiveDriver ?? false;
	}
}
