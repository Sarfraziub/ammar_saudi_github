using Application.Features.Common.Interfaces;
using Application.User.Commands.SendPhoneCodeToken.V1;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.User.Commands.Access.V1;

public class Handler : IRequestHandler<AccessCommand, long>
{
	private readonly IErrorMessagesService _errorMessagesService;
	private readonly IConfiguration _configuration;
	private readonly IMediator _mediator;
	private readonly IUserManager _userManager;
    private readonly ICurrentUserService _currentUserService;

    public Handler(
		IUserManager userManager
		, IMediator mediator
		, IErrorMessagesService errorMessagesService
		, IConfiguration configuration, 
        ICurrentUserService currentUserService
        )
	{
		_userManager = userManager;
		_mediator = mediator;
		_errorMessagesService = errorMessagesService;
		_configuration = configuration;
        _currentUserService = currentUserService;
    }

	public async Task<long> Handle(AccessCommand request, CancellationToken cancellationToken)
	{
		long userId = 0;

        await _mediator.Send(new SendPhoneCodeConfirmationCommand { PhoneNumber = request.PhoneNumber },
			cancellationToken);

		return userId;
	}
}
