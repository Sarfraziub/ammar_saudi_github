using MediatR;

namespace Application.Drivers.Commands.UpdateMyProfileAsDriver;

public class UpdateMyProfileAsDriverCommand : IRequest<Unit>
{
	public string Email { get; set; }
	public string Name { get; set; }
	public string NationalId { get; set; }
	public string Iban { get; set; }
	public string BankName { get; set; }
	public long? NationalImageImageId { get; set; }
	public long? IbanImageId { get; set; }
}
