using MediatR;

namespace Application.Location.Commands.DeleteLocation;

public class DeleteLocationCommand : IRequest<Unit>
{
	public long Id { get; set; }
}


