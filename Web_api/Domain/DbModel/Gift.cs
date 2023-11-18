
namespace Domain.DbModel
{
    public class Gift : Entity
    {
        public string ReceiverName { get; set; }
        public string SenderName { get; set; }
        public string PhoneNumber { get; set; }
        public long ClientOrderId { get; set; }
        public virtual ClientOrder ClientOrder { get; set; }
    }
}
