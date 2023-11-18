using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;

namespace Application.Features.LinkGeneration.Query.GetAllLinkGeneration
{
    public class GetAllLinkGenerationResponse : IMapFrom<Domain.DbModel.LinkGeneration>
    {
        public string UniqueId { get; set; }
        public bool IsValid { get; set; }
        public LinkGenerationType LinkGenerationType { get; set; }
        public bool SendWhatsapp { get; set; }
        public bool SendEmail { get; set; }
        public long UserId { get; set; }
        public List<LinkGenerationValue> LinkGenerationValue { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.DbModel.LinkGeneration, GetAllLinkGenerationResponse>();
            profile.CreateMap<Domain.DbModel.LinkGenerationValue, LinkGenerationValue>();
        }
    }

    public class LinkGenerationValue
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string LinkId { get; set; }
    }
}

