using MediatR;

namespace Application.Features.PromoCodes.Commands.DeletePromoCode;

public class DeletePromoCodeCommand : IRequest<Unit>
{
	public long Id { get; set; }
}


