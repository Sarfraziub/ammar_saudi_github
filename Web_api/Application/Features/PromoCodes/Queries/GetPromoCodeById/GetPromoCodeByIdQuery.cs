using MediatR;

namespace Application.Features.PromoCodes.Queries.GetPromoCodeById;

public class GetPromoCodeByIdQuery : IRequest<GetPromoCodeByIdDto>
{
	public long Id { get; set; }
}


