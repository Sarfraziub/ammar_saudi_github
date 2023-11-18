using MediatR;

namespace Application.SliderItems.Commands.DeleteSliderItem;

public class DeleteSliderItemCommand : IRequest<Unit>
{
	public long Id { get; set; }
}


