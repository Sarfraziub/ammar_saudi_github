using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Users;
using Application.User.Commands.SendPhoneCodeToken.V2;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.User.Commands.Access.V2;

public class Handler : IRequestHandler<AccessCommand, long>
{
    private readonly IErrorMessagesService _errorMessagesService;
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;
    private readonly IUserManager _userManager;

    public Handler(
        IUserManager userManager
        , IMediator mediator
        , IErrorMessagesService errorMessagesService
        , IConfiguration configuration
    )
    {
        _userManager = userManager;
        _mediator = mediator;
        _errorMessagesService = errorMessagesService;
        _configuration = configuration;
    }

    public async Task<long> Handle(AccessCommand request, CancellationToken cancellationToken)
    {
        long userId;

        var user = await _userManager.FindByPhoneAsync(request.PhoneNumber);

        if (user == null)
        {
            var identityResult =
                await _userManager.CreateUserAsync(new CreateUserModel(request.PhoneNumber, ApplicationRoles.User,
                    false));
            if (!identityResult.Succeeded) throw new AppBadRequestException("Error");
            // user = await _context
            // 	.Users
            // 	.SingleOrDefaultAsync(applicationUser => applicationUser.PhoneNumber == phone,
            // 		cancellationToken: cancellationToken);
            user = await _userManager.FindByPhoneAsync(request.PhoneNumber);
            if (user == null) throw new AppBadRequestException("Error");
            if (user.LockoutEnd == null)
                userId = user.Id;
            else
                throw new AppBadRequestException(_errorMessagesService.GetAccountErrorMessageById(1));
        }
        else
        {
            if (user.LockoutEnd != null)
                throw new AppBadRequestException(_errorMessagesService.GetAccountErrorMessageById(1));
            userId = user.Id;
        }

        if (bool.Parse(_configuration["development"]))
        {
            if (user.UserName is "00966700000000" or "00966700000001" or "00966700000002")
            {
                return userId;
            }
        }


        await _mediator.Send(new SendPhoneCodeConfirmationCommand { PhoneNumber = request.PhoneNumber },
            cancellationToken);
        return userId;
    }
}
