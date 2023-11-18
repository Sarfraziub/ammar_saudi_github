using Application.Features.Common.Interfaces;
using Domain;
using Domain.DbModel;
using Microsoft.AspNetCore.Identity;

namespace Infrastructures.Identity.Extensions
{
	public class CustomTwoFactorTokenProvider : IUserTwoFactorTokenProvider<ApplicationUser>
	{
		private readonly ITokenStoreService _tokenStoreService;
		public CustomTwoFactorTokenProvider(ITokenStoreService tokenStoreService)
		{
			_tokenStoreService = tokenStoreService;
		}
		public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
		{
			return Task.FromResult(manager.SupportsUserTwoFactor);
		}

		public Task<string> GenerateAsync(string purpose, UserManager<ApplicationUser> manager, ApplicationUser user)
		{
			Random random = new Random();
			int token = random.Next(1000, 9999);

			if (_tokenStoreService.StoreToken(user.Id.ToString(), token.ToString()))
				return Task.FromResult(token.ToString());

			return Task.FromResult(string.Empty);
		}

		public Task<bool> ValidateAsync(string purpose, string token, UserManager<ApplicationUser> manager, ApplicationUser user)
		{
			var storedToken = _tokenStoreService.GetToken(user.Id.ToString());
			if (storedToken != null)
			{
				bool result = storedToken.Equals(token);
				_tokenStoreService.RemoveToken(user.Id.ToString());

				return Task.FromResult(result);
			}
			return Task.FromResult(false);
		}
	}
}
