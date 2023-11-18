using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Features.Common.Models.Users;

namespace Application.Features.Common.Interfaces;

public interface IJwtAuthManager
{
	IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary { get; }
	JwtAuthResult GenerateTokens(string username, string role, string id, string firstLogin);
	JwtAuthResult Refresh(string refreshToken, string accessToken);
	void RemoveExpiredRefreshTokens(DateTime now);
	void RemoveRefreshTokenByUserName(string username);
	(ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token);
}


