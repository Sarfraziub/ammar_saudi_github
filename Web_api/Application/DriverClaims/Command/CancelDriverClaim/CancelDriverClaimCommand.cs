using MediatR;

namespace Application.DriverClaims.Command.CancelDriverClaim;

public class CancelDriverClaimCommand : IRequest<Unit>
{
	public long Id { get; set; }
}
