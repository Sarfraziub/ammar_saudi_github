using MediatR;

namespace Application.User.Commands.SendPhoneCodeToken.V2;

public class SendPhoneCodeConfirmationCommand : IRequest
{
	public string PhoneNumber { get; set; }

	// public bool NewUser { get; set; }
	// public string Username { get; set; }
}


