using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.Drivers.Commands.ChangeDriverActiveStatus;

public class Handler : IRequestHandler<ChangeDriverActiveStatusCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly ICurrentUserService _currentUserService;

	public Handler(IApplicationDbContext context, ICurrentUserService currentUserService)
	{
		_context = context;
		_currentUserService = currentUserService;
	}

	public async Task<Unit> Handle(ChangeDriverActiveStatusCommand request, CancellationToken cancellationToken)
	{
		var driver = await _context.ApplicationUsers
			.FindAsync(_currentUserService.UserId);
		if (driver == null) return Unit.Value;
		if (driver.ActiveDriver == null)
			driver.ActiveDriver = true;

		else
			driver.ActiveDriver = !driver.ActiveDriver;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}
