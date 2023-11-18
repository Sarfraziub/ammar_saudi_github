using MediatR;

namespace Application.DriverClaims.Queries.GetClientOrdersByDriverClaimId;

public class GetClientOrdersByDriverClaimIdQuery : IRequest<GetClientOrdersByDriverClaimIdVm>
{
    public long Id { get; set; }
}