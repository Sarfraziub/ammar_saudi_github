using Microsoft.AspNetCore.Identity;

namespace Infrastructures.Identity.DataProtection;

public class PasswordLessLoginTotpTokenProvider<TUser> : TotpSecurityStampBasedTokenProvider<TUser>
	where TUser : class
{
	public override Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
	{
		return Task.FromResult(false);
	}

	public override async Task<string> GetUserModifierAsync(string purpose, UserManager<TUser> manager, TUser user)
	{
		var email = await manager.GetEmailAsync(user);
		return "PasswordLessLogin:" + purpose + ":" + email;
	}
}


