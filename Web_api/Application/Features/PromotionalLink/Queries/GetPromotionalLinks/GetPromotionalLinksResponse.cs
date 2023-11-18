using Application.Features.Common.Mappings;
using AutoMapper;

namespace Application.Features.PromotionalLink.Queries.GetPromotionalLinks
{
    public class GetPromotionalLinksResponse : IMapFrom<Domain.DbModel.PromotionalLink>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UniqueId { get; set; }
        public string MobileNo { get; set; }
        public string? Email { get; set; }
        public string UniqueName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.DbModel.PromotionalLink, GetPromotionalLinksResponse>();
        }
    }
}