using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.DbModel;
using EnumStringValues;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Clients.Queries.GetClients;

public class Handler : IRequestHandler<GetClientsQuery, GetClientsVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;
	private readonly IUserManager _userManager;

	public Handler(IApplicationDbContext context, IMapper mapper, IUserManager userManager)
	{
		_context = context;
		_mapper = mapper;
		_userManager = userManager;
	}

	public async Task<GetClientsVm> Handle(GetClientsQuery request, CancellationToken cancellationToken)
	{
		var clients = await _userManager.GetUsersInRoleAsync(ApplicationRoles.User.GetStringValue());
		var ids = clients.Select(s => s.Id);
		var vm = new GetClientsVm
		{
			Clients = await _context.ApplicationUsers
				.AsNoTracking()
				.Where(c =>
					ids.Contains(c.Id)
					&& (string.IsNullOrEmpty(request.Name) ||
					    c.Name.Contains(request.Name))
					&& (string.IsNullOrEmpty(request.Username) ||
					    c.UserName.Contains(request.Username))
				)
				.ProjectTo<GetClientDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};

		return vm;
	}
}


