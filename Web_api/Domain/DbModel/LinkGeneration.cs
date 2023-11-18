namespace Domain.DbModel
{
    public class LinkGeneration : Entity
    {
        public string UniqueId { get; set; }
        public bool IsValid { get; set; }
        public int LinkGenerationType { get; set; }
        public bool SendWhatsapp { get; set; }
        public bool SendEmail { get; set; }
        public long UserId { get; set; }

        public ApplicationUser User { get; set; }
        public ICollection<LinkGenerationValue> LinkGenerationValue { get; set; }

    }
}
