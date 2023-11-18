using MediatR;

namespace Application.Drivers.Queries.GetDriverById;

public class GetDriverByIdQuery : IRequest<GetDriverByIdDto>
{
	public long Id { get; set; }
}
