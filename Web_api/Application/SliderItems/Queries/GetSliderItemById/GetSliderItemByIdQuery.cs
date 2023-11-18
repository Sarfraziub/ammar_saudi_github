using MediatR;

namespace Application.SliderItems.Queries.GetSliderItemById;

public class GetSliderItemByIdQuery : IRequest<GetSliderItemByIdDto>
{
	public long Id { get; set; }
}


