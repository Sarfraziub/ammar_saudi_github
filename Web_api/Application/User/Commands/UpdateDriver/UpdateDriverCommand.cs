using MediatR;

namespace Application.User.Commands.UpdateDriver;

public class UpdateDriverCommand : IRequest<Unit>
{
	public long DriverId { get; set; }
	public string Iban { get; set; }
	public string NationalId { get; set; }
	public string BankName { get; set; }
	public long? NationalImageImageId { get; set; }
	public long? IbanImageId { get; set; }
}


