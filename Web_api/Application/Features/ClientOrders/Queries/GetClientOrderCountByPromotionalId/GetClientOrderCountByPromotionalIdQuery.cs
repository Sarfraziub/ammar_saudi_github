using Application.Features.Common.Interfaces;
using Domain.DbModel;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Queries.GetClientOrderCountByPromotionalId
{
    public class GetClientOrderCountByPromotionalIdQuery : IRequest<GetClientOrderCountByPromotionalIdResponse>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? PromotionalId { get; set; }
    }

    public class Validator : AbstractValidator<GetClientOrderCountByPromotionalIdQuery>
    {
        public Validator()
        {
            RuleFor(e => e.PromotionalId)
                .NotNull()
                .NotEmpty();
        }
    }

    public class GetClientOrderCountByPromotionalIdHandler : IRequestHandler<GetClientOrderCountByPromotionalIdQuery, GetClientOrderCountByPromotionalIdResponse>
    {
        private readonly IApplicationDbContext _context;
        public GetClientOrderCountByPromotionalIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetClientOrderCountByPromotionalIdResponse> Handle(GetClientOrderCountByPromotionalIdQuery query, CancellationToken cancellationToken)
        {
            var clientOrdersQuery = _context.ClientOrders
                .Where(x => x.Active == 1 && x.PromotionalLinkId == query.PromotionalId);

            if (query.StartDate != null)
            {
                clientOrdersQuery = clientOrdersQuery.Where(x => x.Created.Date >= query.StartDate);
            }
            if (query.EndDate != null)
            {
                clientOrdersQuery = clientOrdersQuery.Where(x => x.Created.Date <= query.EndDate);
            }

            var result = await clientOrdersQuery.ToListAsync(cancellationToken);

            var response = new GetClientOrderCountByPromotionalIdResponse()
            {
                NewOrderCount = result.Where(x => x.ClientOrderStatus == ClientOrderStatuses.New).Count(),
                PaymentReceivedCount = result
                    .Where(x => x.ClientOrderStatus == ClientOrderStatuses.PaymentReceived).Count(),
                WithDriverCount =
                    result.Where(x => x.ClientOrderStatus == ClientOrderStatuses.WithDriver).Count(),
                DeliveredCount = result.Where(x => x.ClientOrderStatus == ClientOrderStatuses.Delivered).Count()
            };
            return response;
        }
    }
}
