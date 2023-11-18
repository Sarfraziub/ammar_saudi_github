using MediatR;

namespace Application.Features.Clients.Commands.UpdateClientAddress;

public class UpdateClientAddressCommand : IRequest<Unit>
{
	public string Street { get; set; }
	public string City { get; set; }
	public string State { get; set; }
	public long CountryId { get; set; }
	public string ZipCode { get; set; }
}

