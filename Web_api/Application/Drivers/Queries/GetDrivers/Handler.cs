using AutoMapper;
using Domain;
using Domain.DbModel;
using EnumStringValues;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Drivers.Queries.GetDrivers;

public class Handler : IRequestHandler<GetDriversQuery, GetDriversVm>
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

	public async Task<GetDriversVm> Handle(GetDriversQuery request, CancellationToken cancellationToken)
	{
		return new GetDriversVm
		{
			Drivers = _mapper.Map<List<GetDriverDto>>(
					await _userManager.GetUsersInRoleAsync(ApplicationRoles.Driver.GetStringValue()))
				.Where(s =>
					request.ActiveDriver == null || s.ActiveDriver == request.ActiveDriver)
				.ToList()
		};
	}
}
