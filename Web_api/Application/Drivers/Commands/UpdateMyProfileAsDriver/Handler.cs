using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.Drivers.Commands.UpdateMyProfileAsDriver;

public class Handler : IRequestHandler<UpdateMyProfileAsDriverCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly ICurrentUserService _currentUserService;

	public Handler(IApplicationDbContext context, ICurrentUserService currentUserService)
	{
		_context = context;
		_currentUserService = currentUserService;
	}

	public async Task<Unit> Handle(UpdateMyProfileAsDriverCommand request, CancellationToken cancellationToken)
	{
		var driver = await _context.ApplicationUsers
			.FindAsync(_currentUserService.UserId);
		if (driver == null) return Unit.Value;
		driver.Iban = request.Iban;
		driver.BankName = request.BankName;
		driver.NationalId = request.NationalId;
		driver.Name = request.Name;
		driver.Email = request.Email;
		driver.NationalImageImageId = request.NationalImageImageId;
		driver.IbanImageId = request.IbanImageId;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}
