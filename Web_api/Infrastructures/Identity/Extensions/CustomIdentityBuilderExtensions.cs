using Infrastructures.Identity.DataProtection;
using Microsoft.AspNetCore.Identity;

namespace Infrastructures.Identity.Extensions;

public static class CustomIdentityBuilderExtensions
{
	public static IdentityBuilder AddPasswordLessLoginTotpTokenProvider(this IdentityBuilder builder)
	{
		var userType = builder.UserType;
		var totpProvider = typeof(PasswordLessLoginTotpTokenProvider<>).MakeGenericType(userType);
		return builder.AddTokenProvider("PasswordLessLoginTotpProvider", totpProvider);
	}
}


