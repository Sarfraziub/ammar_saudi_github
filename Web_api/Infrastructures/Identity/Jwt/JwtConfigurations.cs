namespace Infrastructures.Identity.Jwt;

public class JwtConfigurations
{
	public string Secret { get; set; }
	public string Purpose { get; set; }
	public string TokenProvider { get; set; }
	public string TwoFactorAuthenticationTokenProvider { get; set; }

	public string Issuer { get; set; }
	public int AccessTokenExpiration { get; set; }
	public int RefreshTokenExpiration { get; set; }
	public string Audience { get; set; }
}


