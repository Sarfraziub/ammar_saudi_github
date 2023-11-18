using MediatR;

namespace Application.ManageClaims.Commands.CloseClaim;

public class CloseClaimCommand : IRequest<Unit>
{
	public long Id { get; set; }
	public long ReceiptId { get; set; }
}
