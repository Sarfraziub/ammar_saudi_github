using Microsoft.AspNetCore.Identity;

namespace Domain.DbModel;

public class ApplicationRole : IdentityRole<long>
{
	public ApplicationRole(string name) : base(name)
	{
	}
}


