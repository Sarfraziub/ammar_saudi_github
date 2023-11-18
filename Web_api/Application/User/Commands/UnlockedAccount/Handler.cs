using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Commands.UnlockedAccount;

public class Handler : IRequestHandler<UnlockedAccountCommand, Unit>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IErrorMessagesService _errorMessagesService;
	private readonly IMediator _mediator;
	private readonly UserManager<ApplicationUser> _userManager;

	public Handler(
		IMediator mediator
		, IErrorMessagesService errorMessagesService
		, UserManager<ApplicationUser> userManager
		, ICurrentUserService currentUserService
	)
	{
		_mediator = mediator;
		_errorMessagesService = errorMessagesService;
		_userManager = userManager;
		_currentUserService = currentUserService;
	}

	public async Task<Unit> Handle(UnlockedAccountCommand request, CancellationToken cancellationToken)
	{
		if (_currentUserService.UserId == request.Id)
			throw new AppBadRequestException(
				"What !");

		var user = await _userManager.FindByIdAsync(request.Id.ToString());
		if (user == null) throw new AppBadRequestException(_errorMessagesService.GetAccountErrorMessageById(2));
		if (user.LockoutEnd == null)
			throw new AppBadRequestException(_errorMessagesService.GetAccountErrorMessageById(4));
		await _userManager.SetLockoutEndDateAsync(user, null);
		return Unit.Value;
	}
}


