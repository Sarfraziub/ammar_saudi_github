using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Application.User.Commands.SendPhoneCodeToken;
using Application.User.Commands.SendPhoneCodeToken.V1;
using Domain;
using Domain.DbModel;
using EnumStringValues;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User.Commands.DriverAccess;

public class Handler : IRequestHandler<DriverAccessCommand, long>
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

	public async Task<long> Handle(DriverAccessCommand request, CancellationToken cancellationToken)
	{
		long userId;
		var roleName = ApplicationRoles.Driver.GetStringValue();
		var users = await _appUserManager.GetUsersInRoleAsync(roleName);
		var user = users.Where(c => c.UserName == request.PhoneNumber).SingleOrDefault();

		if (user == null) throw new AppBadRequestException("Not allowed");
		if (user is { LockoutEnd: { } })
			throw new AppBadRequestException(_errorMessagesService.GetAccountErrorMessageById(1));
		userId = user.Id;

        var token = new Random().Next(100000, 999999).ToString();

        await _mediator.Send(new SendPhoneCodeConfirmationCommand { PhoneNumber = request.PhoneNumber, Token = token},
			cancellationToken);
		return userId;
	}
}


