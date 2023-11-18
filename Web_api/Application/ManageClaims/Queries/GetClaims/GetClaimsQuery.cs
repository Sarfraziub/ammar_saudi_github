using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.ManageClaims.Queries.GetClaims;

public class GetClaimsQuery : IRequest<GetClaimsVm>
{
	public DriverClaimStatuses? DriverClaimStatus { get; set; }

}
