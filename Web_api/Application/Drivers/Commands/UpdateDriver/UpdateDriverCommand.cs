using MediatR;

namespace Application.Drivers.Commands.UpdateDriver;

public class UpdateDriverCommand : IRequest<Unit>
{
	public long DriverId { get; set; }
	public string Iban { get; set; }
	public string NationalId { get; set; }
	public string BankName { get; set; }
	public long? NationalImageImageId { get; set; }
	public long? IbanImageId { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
}
