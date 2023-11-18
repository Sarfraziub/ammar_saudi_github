using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Users;
using Application.User.Commands.Login.V1;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.User.Commands.AccessGuestUser
{
    public class AccessGuestUserCommand : IRequest<LoginResult>
    {
        
    }

    public class AccessGuestUserCommandHandler : IRequestHandler<AccessGuestUserCommand, LoginResult>
    {
        private readonly IErrorMessagesService _errorMessagesService;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IUserManager _userManager;
        private readonly IJwtAuthManager _jwtAuthManager;

        public AccessGuestUserCommandHandler(IUserManager userManager
            , IMediator mediator
            , IErrorMessagesService errorMessagesService
            , IConfiguration configuration, 
            IJwtAuthManager jwtAuthManager)
        {
            _userManager = userManager;
            _mediator = mediator;
            _errorMessagesService = errorMessagesService;
            _configuration = configuration;
            _jwtAuthManager = jwtAuthManager;
        }

        public async Task<LoginResult> Handle(AccessGuestUserCommand request, CancellationToken cancellationToken)
        {
            var tempUserName = Guid.NewGuid().ToString();
            var identityResult = await _userManager.CreateUserAsync(new CreateUserModel(tempUserName, ApplicationRoles.Guest, false));
            if (!identityResult.Succeeded) throw new AppBadRequestException("Error");
            
            var user = await _userManager.FindByNameAsync(tempUserName);
            if (user == null) throw new AppBadRequestException("Error");

            var role = await _userManager.GetRoleAsync(user);
            var token = _jwtAuthManager.GenerateTokens(tempUserName, role, user.Id.ToString(),
                user.FirstLogin.ToString());

            return new LoginResult
            {
                UserName = tempUserName,
                Role = role,
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken.TokenString
            };
        }
    }
}
