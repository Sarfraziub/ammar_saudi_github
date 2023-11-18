using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.User.Commands.UpdateDriver;

public class Handler : IRequestHandler<UpdateDriverCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
	{
		var driver = await _context.ApplicationUsers
			.FindAsync(request.DriverId);
		if (driver == null) return Unit.Value;
		driver.Iban = request.Iban;
		driver.BankName = request.BankName;
		driver.NationalId = request.NationalId;
		if (request.NationalImageImageId != null)
			driver.NationalImageImageId = request.NationalImageImageId;
		if (request.IbanImageId != null)
			driver.IbanImageId = request.IbanImageId;

		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


