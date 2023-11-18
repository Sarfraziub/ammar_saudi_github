
namespace Application.Features.ClientOrders.Queries.GetClientOrderCountByPromotionalId
{
    public class GetClientOrderCountByPromotionalIdResponse
    {
        public int NewOrderCount { get; set; }
        public int PaymentReceivedCount { get; set; }
        public int WithDriverCount { get; set; }
        public int DeliveredCount { get; set; }
    }
}
