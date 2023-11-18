namespace Application.Features.Common.Interfaces;

public interface ICurrentUserService
{
	string Name { get; }

	//string LastName { get; set; }
	string Role { get; }

	string Username { get; }

	//string Username { get; set; }
	long? UserId { get; }
	// long? CompanyId { get; }
	// string CompanyName { get; }
}


