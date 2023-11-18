using MediatR;

namespace Application.Location.Commands.RemoveLocationImage;

public class RemoveLocationImageCommand : IRequest<Unit>
{
	public long Id { get; set; }
}
