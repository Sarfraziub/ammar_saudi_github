namespace Domain.DbModel
{
    public class ActionTrackingHistory : Entity
    {
        public string DeviceId { get; set; }
        public string Ip { get; set; }
        public long UserId { get; set; }
        public long ActionTrackingId { get; set; }
        public long? PromotionalLinkId { get; set; }
        public string? Platform { get; set; }
        public ActionTracking ActionTracking { get; set; }
        public PromotionalLink PromotionalLink { get; set; }
        public ApplicationUser User { get; set; }
    }
}
