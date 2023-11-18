using MediatR;

namespace Application.User.Commands.SendPhoneCodeToken.V1;

public class SendPhoneCodeConfirmationCommand : IRequest
{
	public string PhoneNumber { get; set; } 
	public string? Token { get; set; }
}


