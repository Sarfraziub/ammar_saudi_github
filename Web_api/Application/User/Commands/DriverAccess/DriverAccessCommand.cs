using MediatR;

namespace Application.User.Commands.DriverAccess;

public class DriverAccessCommand : IRequest<long>
{
	public string PhoneNumber { get; set; }
	// public string Password { get; set; }
}


