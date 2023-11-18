using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Application.User.Commands.SendPhoneCodeToken.V2;
using Domain;
using Domain.DbModel;
using EnumStringValues;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Commands.AdminAccess.V2;

public class Handler : IRequestHandler<AdminAccessCommand, long>
{
	private readonly UserManager<ApplicationUser> _appUserManager;
	private readonly IErrorMessagesService _errorMessagesService;
	private readonly IMediator _mediator;

	public Handler(
		IMediator mediator
		, IErrorMessagesService errorMessagesService
		, UserManager<ApplicationUser> appUserManager
	)
	{
		_mediator = mediator;
		_errorMessagesService = errorMessagesService;
		_appUserManager = appUserManager;
	}

	public async Task<long> Handle(AdminAccessCommand request, CancellationToken cancellationToken)
	{
		long userId;
		var roleName = ApplicationRoles.Admin.GetStringValue();
		var users = await _appUserManager.GetUsersInRoleAsync(roleName);
		var user = users.Where(c => c.UserName == request.PhoneNumber).SingleOrDefault();

		if (user == null) throw new AppBadRequestException("Not allowed");
		if (user is { LockoutEnd: { } })
			throw new AppBadRequestException(_errorMessagesService.GetAccountErrorMessageById(1));
		userId = user.Id;


		await _mediator.Send(new SendPhoneCodeConfirmationCommand { PhoneNumber = request.PhoneNumber },
			cancellationToken);
		return userId;
	}
}


