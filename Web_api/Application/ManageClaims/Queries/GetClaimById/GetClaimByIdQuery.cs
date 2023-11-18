using MediatR;

namespace Application.ManageClaims.Queries.GetClaimById;

public class GetClaimByIdQuery : IRequest<GetClaimByIdDto>
{
	public long Id { get; set; }
}
