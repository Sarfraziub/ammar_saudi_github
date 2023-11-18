using MediatR;

namespace Application.User.Commands.AdminAccess.V2;

public class AdminAccessCommand : IRequest<long>
{
	public string PhoneNumber { get; set; }
	// public string Password { get; set; }
}


