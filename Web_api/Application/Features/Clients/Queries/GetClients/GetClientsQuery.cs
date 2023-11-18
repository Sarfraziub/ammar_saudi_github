using MediatR;

namespace Application.Features.Clients.Queries.GetClients;

public class GetClientsQuery : IRequest<GetClientsVm>
{
	public string Name { get; set; }
	public string Username { get; set; }
}


