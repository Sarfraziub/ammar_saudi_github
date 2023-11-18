namespace Domain.DbModel;

public class Address : Entity
{
	public ApplicationUser User { get; set; }
	public long UserId { get; set; }
}

