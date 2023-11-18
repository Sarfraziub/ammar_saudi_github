using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Users;
using Domain;
using Domain.DbModel;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Drivers.Commands.AddDriver;

public class Handler : IRequestHandler<AddDriverCommand, Unit>
{
	private readonly IUserManager _userManager;
	private readonly IApplicationDbContext _context;

	public Handler(IUserManager userManager, IApplicationDbContext context)
	{
		_userManager = userManager;
		_context = context;
	}

	public async Task<Unit> Handle(AddDriverCommand request, CancellationToken cancellationToken)
	{
		var result =
			await _userManager.CreateUserAsync(new CreateUserModel(request.PhoneNumber, ApplicationRoles.Driver,
				false));
		if (result.Succeeded)
		{
			var driver = await _context.ApplicationUsers
				.Where(s => s.UserName == request.PhoneNumber).SingleAsync(cancellationToken);
			driver.Iban = request.Iban;
			driver.BankName = request.BankName;
			driver.NationalId = request.NationalId;
			driver.Name = request.Name;
			driver.Email = request.Email;
			driver.NationalImageImageId = request.NationalImageImageId;
			driver.IbanImageId = request.IbanImageId;
			await _context.SaveChangesAsync(cancellationToken);
			return Unit.Value;
		}

		var failures = result.Errors.Select(error => new ValidationFailure(error.Code, error.Description))
			.ToList();
		throw new AppValidationException(failures);
	}
}
