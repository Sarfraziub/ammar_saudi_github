using MediatR;

namespace Application.Location.Queries.GetLocationById;

public class GetLocationByIdQuery : IRequest<GetLocationByIdDto>
{
	public long Id { get; set; }
}


