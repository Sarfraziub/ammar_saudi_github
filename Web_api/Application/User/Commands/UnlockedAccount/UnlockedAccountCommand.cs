using MediatR;

namespace Application.User.Commands.UnlockedAccount;

public class UnlockedAccountCommand : IRequest<Unit>
{
	public long Id { get; set; }
}


