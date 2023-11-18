using Application.Features.Common.Models.Users;
using Domain.DbModel;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Common.Interfaces;

public interface IUserManager
{
	// Task<ApplicationUser> GetUserProfile(string email);
	Task<IdentityResult> CreateUserAsync(CreateUserModel model);
	// Task<IdentityResult> CreateUserAsync(RegisterCompanyOwnerUserModel registerModel);
	// Task<IdentityResult> RegisterCompanyOwnerUser(RegisterCompanyOwnerUserCommand registerModel);
	// Task<IdentityResult> RegisterSystemAdminUser(RegisterSystemAdminCommand registerModel);

	// Task<bool> Login(LoginModel loginModel);
	// Task<bool> Login(AccessCommand command);

	Task<IdentityResult> DeleteAsync(string email);

	Task<ApplicationUser> FindByNameAsync(string username);
	Task<ApplicationUser> FindByPhoneAsync(string phone);
	Task<ApplicationUser> FindByActivatedPhoneAsync(string phone);
	Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

	Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);

	Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token);
	Task<IdentityResult> ConfirmPhoneAsync(ApplicationUser user);

	// Task<ApplicationUser> FindByIdAsync(string id);
	Task<ApplicationUser> FindByEmailAsync(string email);

	Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);

	Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token,
		string newPassword);

	Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);

	Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user);

	Task<IList<string>> GetValidTwoFactorProvidersAsync(ApplicationUser user);

	Task<string> GenerateTwoFactorTokenAsync(ApplicationUser user);

    Task<bool> VerifyOtp(string phoneNumber, string token);
    Task<bool> VerifyTwoFactorSixDigitsTokenAsync(ApplicationUser user, string token);
	Task<bool> VerifyTwoFactorFourDigitsTokenAsync(ApplicationUser user, string token);


	// Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(ApplicationUser user, int number);

	Task<string> GenerateJwt(ApplicationUser user);

	// Task<IdentityResult> ChangeEmailAsync(ApplicationUser user, string newEmail, string token);

	// Task<IdentityResult> SetPhoneNumberAsync(ApplicationUser user, string phoneNumber);

	Task<string> GenerateUserSixDigitsTokenAsync(ApplicationUser user);
	Task<string> GenerateUserFourDigitsTokenAsync(ApplicationUser user);

	// Task<string> GetAuthenticationTokenAsync(ApplicationUser user, string loginProvider, string tokenName);

	Task<bool> VerifyUserTokenAsync(ApplicationUser user, string token);

	// Task<IdentityResult> AddToRoleAsync(ApplicationUser user, ApplicationRoles applicationRole);

	// Task<string[]> GetRolesAsync(ApplicationUser user);
	Task<string> GetRoleAsync(ApplicationUser user);

	Task<List<string>> GetRolesAsync(ApplicationUser user);

	Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName);
}


