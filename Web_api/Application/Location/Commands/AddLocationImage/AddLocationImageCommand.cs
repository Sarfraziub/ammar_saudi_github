using MediatR;

namespace Application.Location.Commands.AddLocationImage;

public class AddLocationImageCommand : IRequest<Unit>
{
	public long ImageId { get; set; }
	public long LocationId { get; set; }
}
