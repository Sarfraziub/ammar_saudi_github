using Application.Features.Common.Interfaces;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Commands.UpdateMyProfile;

public class Handler : IRequestHandler<UpdateMyProfileCommand, Unit>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly UserManager<ApplicationUser> _userManager;

	public Handler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService)
	{
		_userManager = userManager;
		_currentUserService = currentUserService;
	}

	public async Task<Unit> Handle(UpdateMyProfileCommand request, CancellationToken cancellationToken)
	{
		var user = await _userManager.FindByIdAsync(_currentUserService.UserId.ToString());
		if (!string.IsNullOrEmpty(request.Name)) user.Name = request.Name;
		if (!string.IsNullOrEmpty(request.Email)) user.Email = request.Email;
		if (request.ImageId != null) user.ImageId = request.ImageId;
		user.Language = request.Language;
		await _userManager.UpdateAsync(user);
		return Unit.Value;
	}
}


