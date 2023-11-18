using System.Text.Json.Serialization;

namespace Application.Features.Common.Models.Users;

public class JwtAuthResult
{
	[JsonPropertyName("accessToken")] public string AccessToken { get; set; }

	[JsonPropertyName("refreshToken")] public RefreshToken RefreshToken { get; set; }
}


