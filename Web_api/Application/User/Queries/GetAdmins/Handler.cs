using AutoMapper;
using Domain;
using Domain.DbModel;
using EnumStringValues;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Queries.GetAdmins;

public class Handler : IRequestHandler<GetAdminsQuery, AdminsVm>
{
	private readonly IMapper _mapper;
	private readonly UserManager<ApplicationUser> _userManager;

	public Handler(
		IMapper mapper
		, UserManager<ApplicationUser> userManager
	)
	{
		_mapper = mapper;
		_userManager = userManager;
	}

	public async Task<AdminsVm> Handle(GetAdminsQuery request, CancellationToken cancellationToken)
	{
		// var users = ;
		// var admins =
		return new AdminsVm
		{
			Admins = _mapper.Map<List<AdminDto>>(
				await _userManager.GetUsersInRoleAsync(ApplicationRoles.Admin.GetStringValue()))
		};
	}
}


