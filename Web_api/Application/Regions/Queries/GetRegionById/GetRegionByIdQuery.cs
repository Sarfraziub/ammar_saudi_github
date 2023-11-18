using MediatR;

namespace Application.Regions.Queries.GetRegionById;

public class GetRegionByIdQuery : IRequest<GetRegionByIdDto>
{
	public long Id { get; set; }
}


