using MediatR;

namespace Application.Regions.Commands.UpdateRegion;

public class UpdateRegionCommand : IRequest<Unit>
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string ArabicName { get; set; }
}


