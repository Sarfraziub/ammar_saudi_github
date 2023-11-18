namespace Application.User.Commands.Login.V2;

public class LoginResult
{
    // [JsonPropertyName("username")]
    public string UserName { get; set; }

    // [JsonPropertyName("role")]
    public string Role { get; set; }

    // [JsonPropertyName("originalUserName")]
    // public string OriginalUserName { get; set; }

    // [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; }

    // [JsonPropertyName("refreshToken")]
    public string RefreshToken { get; set; }
    public List<GuestOrderVm> ClientOrders { get; set; }
}

public class GuestOrderVm
{
    public long Id { get; set; }
    public string Number { get; set; }
    public string ClientOrderStatus { get; set; }
    public decimal Cost { get; set; }
    public string? ServiceType { get; set; }

}
