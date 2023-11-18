using EnumStringValues;

namespace Domain.DbModel;

public enum ApplicationRoles
{
	[StringValue("Admin")] Admin = 1,
	[StringValue("User")] User = 2,
	[StringValue("Finance")] Finance = 3,
	[StringValue("Driver")] Driver = 4,
	[StringValue("Guest")] Guest = 5,
}

// public static class Roles
// {
//     public const string Admin = "Admin";
//     public const string CompanyOwner = "CompanyOwner";
//     public const string CompanyRepresentative = "CompanyRepresentative";
// }


