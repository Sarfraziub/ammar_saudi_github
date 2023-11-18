using MediatR;

namespace Application.User.Commands.Access.V1;

public class AccessCommand : IRequest<long>
{
	public string PhoneNumber { get; set; }
	// public string Password { get; set; }
}


