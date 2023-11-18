
namespace Domain.DbModel
{
    public class PromotionalLink : Entity
    {
        public string Name { get; set; }
        public string UniqueId { get; set; }
        public string MobileNo { get; set; }
        public string? Email { get; set; }
        public string UniqueName { get; set; }
        public bool Status { get; set; }
    }
}
