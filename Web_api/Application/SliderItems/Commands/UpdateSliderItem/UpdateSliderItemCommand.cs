using MediatR;

namespace Application.SliderItems.Commands.UpdateSliderItem;

public class UpdateSliderItemCommand : IRequest<Unit>
{
	public long Id { get; set; }
	public string Name { get; set; }
	public long ImageId { get; set; }
	public bool Visible { get; set; }
	public int Order { get; set; }
}


