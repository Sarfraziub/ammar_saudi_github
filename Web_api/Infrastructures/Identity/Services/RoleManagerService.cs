// using Application.Common.Interfaces;
// using Domain;
// using Microsoft.AspNetCore.Identity;
//
// namespace Infrastructures.Identity.Services;
//
// public class RoleManagerService : IRoleManager
// {
// 	private readonly RoleManager<ApplicationRole> _roleManager;
//
// 	public RoleManagerService(RoleManager<ApplicationRole> roleManager)
// 	{
// 		_roleManager = roleManager;
// 	}
//
// 	public async Task CreateRoleAsync(string roleName)
// 	{
// 		var identityRole = new ApplicationRole
// 		{
// 			Name = roleName
// 		};
// 		await _roleManager.CreateAsync(identityRole).ConfigureAwait(false);
// 	}
//
// 	public IEnumerable<ApplicationRole> GetRoles()
// 	{
// 		return _roleManager.Roles.ToList();
// 	}
// }



