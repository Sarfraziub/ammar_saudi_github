using Application.Features.Common.Interfaces;
using Domain.DbModel;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Clients.Commands.UpdateClientAddress;

public class Handler : IRequestHandler<UpdateClientAddressCommand, Unit>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly UserManager<ApplicationUser> _userManager;

	public Handler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService)
	{
		_userManager = userManager;
		_currentUserService = currentUserService;
	}

	public async Task<Unit> Handle(UpdateClientAddressCommand request, CancellationToken cancellationToken)
	{
		var user = await _userManager.FindByIdAsync(_currentUserService.UserId.ToString());
		user.City = request.City;
		user.State = request.State;
		user.Street = request.Street;
		user.CountryId = request.CountryId;
		user.ZipCode = request.ZipCode;
		await _userManager.UpdateAsync(user);
		return Unit.Value;
	}
}


