using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.User.Commands.UpdateMyProfile;

public class UpdateMyProfileCommand : IRequest<Unit>
{
	public string Name { get; set; }
	public string Email { get; set; }
	public long? ImageId { get; set; }

	public Languages Language { get; set; }
}


