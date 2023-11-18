using Domain.DbModel;

namespace Application.Features.Common.Models.Users;

public class CreateUserModel
{
	public CreateUserModel(string phoneNumber, ApplicationRoles applicationRole, bool phoneNumberConfirmed)
	{
		PhoneNumber = phoneNumber;
		Role = applicationRole;
		PhoneNumberConfirmed = phoneNumberConfirmed;
	}

	// public string Name { get; set; }
	// public string Username { get; set; }
	// public string Password { get; set; }
	public string PhoneNumber { get; set; }

	public ApplicationRoles Role { get; set; }

	// public bool EmailConfirmed { get; set; }
	public bool PhoneNumberConfirmed { get; set; }
}


