using MediatR;

namespace Application.User.Commands.AddDriver;

public class AddDriverCommand : IRequest<Unit>
{
	public string PhoneNumber { get; set; }
}


