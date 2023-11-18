using MediatR;

namespace Application.User.Commands.LockoutAccount;

public class LockoutAccountCommand : IRequest<Unit>
{
	public long Id { get; set; }
}


