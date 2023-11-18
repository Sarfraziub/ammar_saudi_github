
namespace Domain.DbModel
{
    public class LinkGenerationValue : Entity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public long LinkGenerationId { get; set; }
        public LinkGeneration LinkGeneration { get; set; }

    }
}
