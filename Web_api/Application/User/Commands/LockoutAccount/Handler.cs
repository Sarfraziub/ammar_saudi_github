using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Commands.LockoutAccount;

public class Handler : IRequestHandler<LockoutAccountCommand, Unit>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IErrorMessagesService _errorMessagesService;
	private readonly UserManager<ApplicationUser> _userManager;

	public Handler(
		IErrorMessagesService errorMessagesService
		, UserManager<ApplicationUser> userManager
		, ICurrentUserService currentUserService
	)
	{
		_errorMessagesService = errorMessagesService;
		_userManager = userManager;
		_currentUserService = currentUserService;
	}

	public async Task<Unit> Handle(LockoutAccountCommand request, CancellationToken cancellationToken)
	{
		if (_currentUserService.UserId == request.Id)
			throw new AppValidationException(_errorMessagesService.GetCommonErrorMessageById(4));
		var user = await _userManager.FindByIdAsync(request.Id.ToString());
		if (user == null) throw new AppBadRequestException(_errorMessagesService.GetAccountErrorMessageById(2));
		if (user.LockoutEnd != null)
			throw new AppBadRequestException(_errorMessagesService.GetAccountErrorMessageById(3));
		await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(10));
		return Unit.Value;
	}
}


