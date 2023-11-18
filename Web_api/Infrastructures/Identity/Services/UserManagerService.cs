using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Users;
using Domain;
using Domain.DbModel;
using Infrastructures.Identity.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace Infrastructures.Identity.Services;

public class UserManagerService : IUserManager
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _context;

    public UserManagerService(
		UserManager<ApplicationUser> userManager
		, IOptions<JwtConfigurations> options, 
        IApplicationDbContext context
    )
	{
		_userManager = userManager;
        _context = context;
        Options = options.Value;
	}

	private JwtConfigurations Options { get; }

	// public Task<ApplicationUser> GetUserProfile(string email)
	// {
	//     throw new NotImplementedException();
	// }

	public async Task<IdentityResult> CreateUserAsync(CreateUserModel model)
	{
		const string password = "P@ssw0rd@123";
		// var phone = _formater.FormatPhoneNumberForSaudiMobile(model.PhoneNumber);
		var userWithThisPhone = await FindByActivatedPhoneAsync(model.PhoneNumber);
		if (userWithThisPhone != null) throw new AppBadRequestException("this phone number already there !");
		var user = new ApplicationUser
		{
			UserName = model.PhoneNumber,
			TwoFactorEnabled = true,
			PhoneNumber = model.PhoneNumber,
			PhoneNumberConfirmed = model.PhoneNumberConfirmed,
			EmailConfirmed = false,
			Name = string.Empty
		};
		var result = await _userManager.CreateAsync(user, password);
		if (result.Succeeded) return await AddToRoleAsync(user, model.Role);
		var failures = result.Errors.Select(error =>
				new ValidationFailure(error.Code, error.Description))
			.ToList();
		throw new AppValidationException(failures);
	}

	public async Task<IdentityResult> DeleteAsync(string email)
	{
		return await _userManager.DeleteAsync(await _userManager.FindByEmailAsync(email));
	}

	public async Task<ApplicationUser> FindByNameAsync(string username)
	{
		return await _userManager.FindByNameAsync(username);
	}

	public async Task<ApplicationUser> FindByPhoneAsync(string phone)
	{
		// var formatPhone = _formater.FormatPhoneNumberForSaudiMobile(phone);
		return await _userManager.Users.SingleOrDefaultAsync(user => user.PhoneNumber == phone);
	}

	public async Task<ApplicationUser> FindByActivatedPhoneAsync(string phone)
	{
		return await _userManager.Users.SingleOrDefaultAsync(user =>
			user.PhoneNumber == phone);
	}

	// public async Task<ApplicationUser> FindByIdAsync(string id)
	// {
	//     return await _userManager.FindByIdAsync(id);
	// }
	public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
	{
		return await _userManager.CheckPasswordAsync(user, password);
	}

	public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
	{
		return await _userManager.GenerateEmailConfirmationTokenAsync(user);
		//return await _userManager.GenerateUserTokenAsync(user);
	}

	public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
	{
		return await _userManager.ConfirmEmailAsync(user, token);
	}

	public async Task<IdentityResult> ConfirmPhoneAsync(ApplicationUser user)
	{
		user.PhoneNumberConfirmed = true;
		return await _userManager.UpdateAsync(user);
	}

	public async Task<ApplicationUser> FindByEmailAsync(string email)
	{
		return await _userManager.FindByEmailAsync(email);
	}

	public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
	{
		return await _userManager.GeneratePasswordResetTokenAsync(user);
	}

	public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token,
		string newPassword)
	{
		return await _userManager.ResetPasswordAsync(user, token, newPassword);
	}

	public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword,
		string newPassword)
	{
		return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
	}

	public async Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user)
	{
		return await _userManager.GetTwoFactorEnabledAsync(user);
	}

	public async Task<IList<string>> GetValidTwoFactorProvidersAsync(ApplicationUser user)
	{
		return await _userManager.GetValidTwoFactorProvidersAsync(user);
	}

	public async Task<string> GenerateTwoFactorTokenAsync(ApplicationUser user)
	{
		return await _userManager.GenerateTwoFactorTokenAsync(user, Options.TokenProvider);
	}

	public async Task<bool> VerifyTwoFactorSixDigitsTokenAsync(ApplicationUser user, string token)
	{
		//return await _userManager.VerifyTwoFactorTokenAsync(user, Options.TokenProvider, token);
		return await _userManager.VerifyTwoFactorTokenAsync(user, Options.TokenProvider, token);
	}
	public async Task<bool> VerifyOtp(string phoneNumber, string token)
    {
        var result = await _context.Otps
            .Where(x => x.PhoneNumber == phoneNumber)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();

        return result?.Code == token;
    }

	public async Task<bool> VerifyTwoFactorFourDigitsTokenAsync(ApplicationUser user, string token)
	{
		return await _userManager.VerifyTwoFactorTokenAsync(user, Options.TwoFactorAuthenticationTokenProvider, token);
	}

	public async Task<string> GenerateJwt(ApplicationUser user)
	{
		var role = await GetRoleAsync(user);
		//var srole = JsonConvert.SerializeObject(roles).Replace("\"", "");
		//var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

		// return null if user not found
		// if (user == null)
		//     return null;

		// authentication successful so generate jwt token
		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(Options.Secret);
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Role, role)
				//new Claim(ClaimTypes.Name, user.NormalizedUserName),
				//new Claim(ClaimTypes.Sid, user.Id),
				//new Claim(ClaimTypes.NameIdentifier, user.CompanyId.ToString()),
			}),
			Expires = DateTime.UtcNow.AddDays(7),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
				SecurityAlgorithms.HmacSha256Signature)
		};
		var securityToken = tokenHandler.CreateToken(tokenDescriptor);
		var token = tokenHandler.WriteToken(securityToken);

		return token;
	}

	//public string Authenticate(ApplicationUser user)
	//{
	//    //var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

	//    // return null if user not found
	//    if (user == null)
	//        return null;
	//    var role =

	//    var key = Encoding.ASCII.GetBytes(Options.JwtSecret);
	//    var securityTokenDescriptor = new SecurityTokenDescriptor
	//    {
	//        Subject = new ClaimsIdentity(new Claim[]
	//        {
	//            new Claim(ClaimTypes.Name, user.Id.ToString()),
	//            new Claim(ClaimTypes.Role, GetRolesAsync(user).ToString())
	//        }),
	//        Expires = DateTime.UtcNow.AddDays(7),
	//        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
	//    };
	//    var tokenHandler = new JwtSecurityTokenHandler();
	//    var securityToken = tokenHandler.CreateToken(securityTokenDescriptor);
	//    var token = tokenHandler.WriteToken(securityToken);
	//    return token;
	//}

	// public Task<IdentityResult> ChangeEmailAsync(ApplicationUser user, string newEmail, string token)
	// {
	//     throw new NotImplementedException();
	// }

	// public Task<IdentityResult> SetPhoneNumberAsync(ApplicationUser user, string phoneNumber)
	// {
	//     throw new NotImplementedException();
	// }

	public async Task<string> GenerateUserSixDigitsTokenAsync(ApplicationUser user)
	{
		var token = await _userManager.GenerateUserTokenAsync(user, Options.TokenProvider, Options.Purpose);
		return token;
	}

	public async Task<string> GenerateUserFourDigitsTokenAsync(ApplicationUser user)
	{
		var token = await _userManager.GenerateUserTokenAsync(user, Options.TwoFactorAuthenticationTokenProvider,
			Options.Purpose);
		return token;
	}

	// public async Task<string> GetAuthenticationTokenAsync(ApplicationUser user, string loginProvider,
	//     string tokenName)
	// {
	//     return await _userManager.GetAuthenticationTokenAsync(user, loginProvider, tokenName);
	// }

	//public async Task<Result> DeleteUserAsync(ApplicationUser user)
	//{
	//    var result = await _userManager.DeleteAsync(user);
	//    //_userManager.email()
	//    return result.ToApplicationResult();
	//}

	public async Task<bool> VerifyUserTokenAsync(ApplicationUser user, string token)
	{
		return await _userManager.VerifyUserTokenAsync(user, Options.TokenProvider, Options.Purpose, token);
	}

	// public async Task<string[]> GetRolesAsync(ApplicationUser user)
	// {
	//     var roles = await _userManager.GetRolesAsync(user);
	//     var array = roles.ToArray();
	//     return array;
	// }

	public async Task<string> GetRoleAsync(ApplicationUser user)
	{
		var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
		var role = roles.Single();
		return role;
	}

	public async Task<List<string>> GetRolesAsync(ApplicationUser user)
	{
		var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
		return (List<string>)roles;
	}

	public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName)
	{
		return await _userManager.GetUsersInRoleAsync(roleName);
	}


	// public Task<IdentityResult> CreateUserAsync(RegisterCompanyOwnerUserModel registerModel, ApplicationRoles role)
	// {
	//     throw new NotImplementedException();
	// }

	// public async Task<IdentityResult> CreateUserAsync(RegisterCompanyOwnerUserModel registerModel)
	// {
	//     var user = new ApplicationUser
	//     {
	//         UserName = registerModel.Username,
	//         Username = registerModel.Username,
	//         //CompanyId = registerModel.CompanyId,
	//         TwoFactorEnabled = true
	//     };
	//
	//     return await _userManager.CreateAsync(user, registerModel.Password);
	// }

	// public async Task<IdentityResult> RegisterCompanyOwnerUser(RegisterCompanyOwnerUserCommand command)
	// {
	//     var user = new ApplicationUser
	//     {
	//         UserName = command.Username,
	//         Username = command.Username,
	//         TwoFactorEnabled = true,
	//         PhoneNumber = command.PhoneNumber,
	//         PhoneNumberConfirmed = false
	//     };
	//
	//     var result = await _userManager.CreateAsync(user, command.Password);
	//     if (!result.Succeeded) return result;
	//     var addRoleResult = await AddToRoleAsync(user, ApplicationRoles.CompanyOwner);
	//     if (addRoleResult.Succeeded)
	//         return addRoleResult;
	//     await DeleteAsync(command.Username);
	//     return result;
	// }
	//
	// public async Task<IdentityResult> RegisterSystemAdminUser(RegisterSystemAdminCommand command)
	// {
	//     var user = new ApplicationUser
	//     {
	//         UserName = command.Username,
	//         Username = command.Username,
	//         TwoFactorEnabled = true,
	//         EmailConfirmed = command.EmailConfirmed,
	//         PhoneNumber = command.PhoneNumber,
	//         PhoneNumberConfirmed = true
	//     };
	//
	//     var result = await _userManager.CreateAsync(user, command.Password);
	//     if (!result.Succeeded) return result;
	//     var addRoleResult = await AddToRoleAsync(user, ApplicationRoles.Admin);
	//     if (addRoleResult.Succeeded)
	//         return addRoleResult;
	//     await DeleteAsync(command.Username);
	//     return result;
	// }

	// public async Task<Result> DeleteUserAsync(string userId)
	// {
	//     var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
	//     if (user != null) return await DeleteUserAsync(user);
	//     return Result.Success();
	// }


	// public async Task<bool> Login(LoginModel loginModel)
	// {
	// 	var user = await _userManager.FindByNameAsync(loginModel.Username);
	// 	if (user == null) return false;
	// 	var validPassword = await _userManager.CheckPasswordAsync(user, loginModel.Password);
	// 	return validPassword;
	// }

	public async Task<ApplicationUser> FindByIdAsync(string id)
	{
		return await _userManager.FindByIdAsync(id);
	}

	public async Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(ApplicationUser user, int number)
	{
		return await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, number);
	}

	public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, ApplicationRoles applicationRole)
	{
		return await _userManager.AddToRoleAsync(user, Enum.GetName(typeof(ApplicationRoles), applicationRole));
	}

	// public async Task<string> GenerateJwt1(ApplicationUser user)
	// {
	//     var role = await GetRoleAsync(user);
	//     //var srole = JsonConvert.SerializeObject(roles).Replace("\"", "");
	//     var securityTokenDescriptor = new SecurityTokenDescriptor
	//     {
	//         Subject = new ClaimsIdentity(new[]
	//         {
	//             new Claim("UserId", user.Id),
	//             new Claim("Username", user.Username),
	//             //new Claim("Roles", srole),
	//             new Claim(ClaimTypes.Role, role)
	//         }),
	//         Expires = DateTime.UtcNow.AddMinutes(5),
	//         SigningCredentials = new SigningCredentials(
	//             new SymmetricSecurityKey(
	//                 //Encoding.UTF8.GetBytes(_configuration["Authentication:EFCore:JwtSecret"])),
	//                 Encoding.UTF8.GetBytes(Options.Secret)),
	//             SecurityAlgorithms.HmacSha256Signature)
	//     };
	//     var tokenHandler = new JwtSecurityTokenHandler();
	//     var securityToken = tokenHandler.CreateToken(securityTokenDescriptor);
	//     var token = tokenHandler.WriteToken(securityToken);
	//     return token;
	// }
}
