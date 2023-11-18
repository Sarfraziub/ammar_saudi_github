using Application.Extentions;
using Application.Features.PromotionalLink.Queries.GetPromotionalLinks;
using Domain;
using Domain.DbModel;
using Sieve.Services;

namespace Application.SieveConfiguration
{
    public class PromotionalLinkSieveConfiguration : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<PromotionalLink>(x => x.Id).CanSortAndFilterByName(nameof(GetPromotionalLinksResponse.Id));
            mapper.Property<PromotionalLink>(x => x.Name).CanSortAndFilterByName(nameof(GetPromotionalLinksResponse.Name));
            mapper.Property<PromotionalLink>(x => x.UniqueId).CanSortAndFilterByName(nameof(GetPromotionalLinksResponse.UniqueId));
            mapper.Property<PromotionalLink>(x => x.MobileNo).CanSortAndFilterByName(nameof(GetPromotionalLinksResponse.MobileNo));
            mapper.Property<PromotionalLink>(x => x.Email).CanSortAndFilterByName(nameof(GetPromotionalLinksResponse.Email));
            mapper.Property<PromotionalLink>(x => x.UniqueName).CanSortAndFilterByName(nameof(GetPromotionalLinksResponse.UniqueName));
        }
    }
}
