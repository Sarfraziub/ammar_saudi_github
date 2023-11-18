using MediatR;

namespace Application.UserDeviceTokens.Commands.DeleteUserDeviceToken;

public class DeleteUserDeviceTokenCommand : IRequest<Unit>
{
	public string Token { get; set; }
}


