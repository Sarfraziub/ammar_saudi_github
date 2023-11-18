using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Users;
using Domain;
using Domain.DbModel;
using FluentValidation.Results;
using MediatR;

namespace Application.User.Commands.AddNewAdmin;

public class Handler : IRequestHandler<AddNewAdminCommand, Unit>
{
	private readonly IUserManager _userManager;

	public Handler(IUserManager userManager)
	{
		_userManager = userManager;
	}

	public async Task<Unit> Handle(AddNewAdminCommand request, CancellationToken cancellationToken)
	{
		var result =
			await _userManager.CreateUserAsync(new CreateUserModel(request.PhoneNumber, ApplicationRoles.Admin, false));
		if (result.Succeeded) return Unit.Value;
		var failures = result.Errors.Select(error => new ValidationFailure(error.Code, error.Description))
			.ToList();
		throw new AppValidationException(failures);
	}
}

