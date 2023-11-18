using Domain.DbModel;

namespace Application.Features.Common.Interfaces;

public interface IRoleManager
{
	Task CreateRoleAsync(string roleName);
	IEnumerable<ApplicationRole> GetRoles();
}


