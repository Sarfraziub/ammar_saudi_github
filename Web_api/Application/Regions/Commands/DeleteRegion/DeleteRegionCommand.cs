using MediatR;

namespace Application.Regions.Commands.DeleteRegion;

public class DeleteRegionCommand : IRequest<Unit>
{
	public long Id { get; set; }
}


