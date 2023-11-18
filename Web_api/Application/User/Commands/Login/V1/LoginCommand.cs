using MediatR;

namespace Application.User.Commands.Login.V1;

public class LoginCommand : IRequest<LoginResult>
{
	public string PhoneNumber { get; set; }
	public string Token { get; set; }
	public string DeviceId { get; set; }
}


