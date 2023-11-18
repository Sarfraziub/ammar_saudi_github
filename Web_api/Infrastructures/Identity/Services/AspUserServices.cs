using System.Security.Claims;
using Application.Features.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructures.Identity.Services;

public class AspUserServices : ICurrentUserService
{
	public AspUserServices(
		//IUserManager userManager
		//,
		IHttpContextAccessor httpContextAccessor
	)
	{
		if (httpContextAccessor.HttpContext != null)
		{
			var claims = httpContextAccessor.HttpContext.User?.Claims;
			var enumerable = claims as Claim[] ?? claims.ToArray();
			//var httpContextAccessor1 = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
			var currentContext = httpContextAccessor.HttpContext;
			if (currentContext.User.Identity != null &&
			    (currentContext == null || !currentContext.User.Identity.IsAuthenticated)) return;
			UserId = long.Parse(httpContextAccessor.HttpContext.User?.Claims?.First(c =>
				c.Type == ClaimTypes.Sid).Value);
			Username = httpContextAccessor.HttpContext.User?.Claims?.First(c =>
				c.Type == ClaimTypes.NameIdentifier).Value;
			Role = httpContextAccessor.HttpContext.User?.Claims?.First(c =>
				c.Type == ClaimTypes.Role).Value;

			// var companyId = enumerable.First(c => c.Type.ToString() == "CompanyId").Value;
			// if (!string.IsNullOrEmpty(companyId))
			// {
			// 	CompanyId = long.Parse(companyId);
			// 	CompanyName = enumerable.First(c => c.Type.ToString() == "CompanyName").Value;
			// }
		}

		//_name = httpContextAccessor.HttpContext.User?.Claims?.First(c =>
		//    c.Type == ClaimTypes.GivenName).Value;
		//_userId = httpContextAccessor.HttpContext.User?.Claims?.First(c =>
		//    c.Type == ClaimTypes.Sid).Value;
		//if (userId == null) return;
		//var user = Task.FromResult(userManager.FindByIdAsync(userId).ConfigureAwait(false)).Result.GetAwaiter()
		//    .GetResult();
		//Username = user.Username;
	}

	//public IHttpContextAccessor _HttpContextAccessor { get; }
	public string Name { get; }

	public string Role { get; }

	public string Username { get; }

	public long? UserId { get; }
	// public long? CompanyId { get; }
	// public string CompanyName { get; }
}


