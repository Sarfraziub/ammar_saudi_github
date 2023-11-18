namespace Domain.DbModel;

public class Otp : Entity
{
	public ApplicationUser User { get; set; }
	public long? UserId { get; set; }
	public string PhoneNumber { get; set; }
	public string Code { get; set; }
	public string OtpResponse { get; set; }
}
