using MediatR;

namespace Application.User.Commands.AddNewAdmin;

public class AddNewAdminCommand : IRequest<Unit>
{
	public string PhoneNumber { get; set; }
}

