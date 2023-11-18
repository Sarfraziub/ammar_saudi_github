using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Users;
using Domain;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructures.Identity.Jwt;

public class JwtAuthManager : IJwtAuthManager
{
	private readonly JwtConfigurations _jwtTokenConfig;
	private readonly byte[] _secret;

    private readonly ConcurrentDictionary<string, RefreshToken>
		_usersRefreshTokens; // can store in a database or a distributed cache

	public JwtAuthManager(JwtConfigurations jwtTokenConfig)
	{
		_jwtTokenConfig = jwtTokenConfig;
        _usersRefreshTokens = new ConcurrentDictionary<string, RefreshToken>();
		_secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret);
	}

	public IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary =>
		_usersRefreshTokens.ToImmutableDictionary();

	// optional: clean up expired refresh tokens
	public void RemoveExpiredRefreshTokens(DateTime now)
	{
		var expiredTokens = _usersRefreshTokens.Where(x => x.Value.ExpireAt < now).ToList();
		foreach (var expiredToken in expiredTokens) _usersRefreshTokens.TryRemove(expiredToken.Key, out _);
	}

	// can be more specific to ip, user agent, device name, etc.
	public void RemoveRefreshTokenByUserName(string userName)
	{
		var refreshTokens = _usersRefreshTokens.Where(x => x.Value.UserName == userName).ToList();
		foreach (var refreshToken in refreshTokens) _usersRefreshTokens.TryRemove(refreshToken.Key, out _);
	}

	public JwtAuthResult GenerateTokens(string username, string role, string id, string firstLogin)
	{
        //525600
        var expiryTime = DateTime.UtcNow;
        var secret = Encoding.ASCII.GetBytes(_jwtTokenConfig.Secret);
		var claims = new[]
		{
			new Claim(ClaimTypes.NameIdentifier, username),
			new Claim(ClaimTypes.Role, role),
			new Claim(ClaimTypes.Sid, id),
			new Claim("FirstLogin", firstLogin)
		};
		var shouldAddAudienceClaim =
			string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);
        
            expiryTime = DateTime.Now.AddMinutes(_jwtTokenConfig.AccessTokenExpiration);
        
		var jwtToken = new JwtSecurityToken(
			_jwtTokenConfig.Issuer,
			shouldAddAudienceClaim ? _jwtTokenConfig.Audience : string.Empty,
			claims,
			expires: expiryTime,
			signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret),
				SecurityAlgorithms.HmacSha256Signature));
		var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

		var refreshToken = new RefreshToken
		{
			UserName = username,
			TokenString = GenerateRefreshTokenString(),
			ExpireAt = DateTime.Now.AddMinutes(_jwtTokenConfig.RefreshTokenExpiration)
		};
		_usersRefreshTokens.AddOrUpdate(refreshToken.TokenString, refreshToken, (_, _) => refreshToken);

		return new JwtAuthResult
		{
			AccessToken = accessToken,
			RefreshToken = refreshToken
		};
	}

	public JwtAuthResult Refresh(string refreshToken, string accessToken)
	{
		var (principal, jwtToken) = DecodeJwtToken(accessToken);
		if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
			throw new SecurityTokenException("Invalid token");

		var userName = ((ClaimsIdentity)principal.Identity)?.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
		var id = ((ClaimsIdentity)principal.Identity)?.Claims.Single(c => c.Type == ClaimTypes.Sid).Value;
		var firstLogin = ((ClaimsIdentity)principal.Identity)?.Claims.Single(c => c.ValueType == "FirstLogin").Value;
		if (!_usersRefreshTokens.TryGetValue(refreshToken, out var existingRefreshToken))
			throw new SecurityTokenException("Invalid token");

		if (existingRefreshToken.UserName != userName || existingRefreshToken.ExpireAt < DateTime.Now)
			throw new SecurityTokenException("Invalid token");

		//var userIdentity = (ClaimsIdentity)principal.Identity;
		// var claims = userIdentity.Claims;
		// var roleClaimType = userIdentity.RoleClaimType;
		var role = ((ClaimsIdentity)principal.Identity)?.Claims.Single(c => c.Type == ClaimTypes.Role).Value;

		return GenerateTokens(userName, role, id, firstLogin); // need to recover the original claims
	}

	public (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token)
	{
		if (string.IsNullOrWhiteSpace(token)) throw new SecurityTokenException("Invalid token");

		var principal = new JwtSecurityTokenHandler()
			.ValidateToken(token,
				new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = _jwtTokenConfig.Issuer,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(_secret),
					ValidAudience = _jwtTokenConfig.Audience,
					ValidateAudience = true,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.FromMinutes(1)
				},
				out var validatedToken);
		return (principal, validatedToken as JwtSecurityToken);
	}

	private static string GenerateRefreshTokenString()
	{
		var randomNumber = new byte[32];
		using var randomNumberGenerator = RandomNumberGenerator.Create();
		randomNumberGenerator.GetBytes(randomNumber);
		return Convert.ToBase64String(randomNumber);
	}
}


