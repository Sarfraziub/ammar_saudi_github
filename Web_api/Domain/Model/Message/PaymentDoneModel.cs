

namespace Domain.Model.Message
{
    public class PaymentDoneModel
    {
        public string Id { get; set; }
        public string OrderNumber { get; set; }
        public string PaymentDate { get; set; }
    }
}
