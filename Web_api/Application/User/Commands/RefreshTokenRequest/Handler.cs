using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.User.Commands.RefreshTokenRequest;

public class Handler : IRequestHandler<RefreshTokenRequestCommand, LoginResult>
{
	private readonly IJwtAuthManager _jwtAuthManager;

	public Handler(IJwtAuthManager jwtAuthManager)
	{
		_jwtAuthManager = jwtAuthManager;
	}

	public Task<LoginResult> Handle(RefreshTokenRequestCommand request, CancellationToken cancellationToken)
	{
		var jwtResult = _jwtAuthManager.Refresh(request.RefreshToken, request.AccessToken);
		return Task.FromResult(new LoginResult
		{
			UserName = request.Username,
			Role = request.Role,
			AccessToken = jwtResult.AccessToken,
			RefreshToken = jwtResult.RefreshToken.TokenString
		});
	}
}


