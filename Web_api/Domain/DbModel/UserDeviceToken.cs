namespace Domain.DbModel;

public class UserDeviceToken : Entity
{
	public ApplicationUser User { get; set; }
	public long UserId { get; set; }
	public DeviceTypes DeviceType { get; set; }
	public string RegistrationToken { get; set; }
	public UserTypes UserType { get; set; }
}