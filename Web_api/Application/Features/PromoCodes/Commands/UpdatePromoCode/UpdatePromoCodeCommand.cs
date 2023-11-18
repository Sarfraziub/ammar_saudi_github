using Domain.Enum;
using MediatR;

namespace Application.Features.PromoCodes.Commands.UpdatePromoCode;

public class UpdatePromoCodeCommand : IRequest<Unit>
{
	public long Id { get; set; }
	public string Code { get; set; }
	public DateTime? Expiry { get; set; }
	public decimal Percentage { get; set; }
	public PromoCodeApplicableType ApplicableType { get; set; }
}


