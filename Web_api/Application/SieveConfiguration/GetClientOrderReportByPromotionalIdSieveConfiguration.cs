using Domain;
using Sieve.Services;
using Application.Extentions;
using Application.Features.ClientOrders.Queries.GetClientOrderReportByPromotionalId;
using Domain.DbModel;

namespace Application.SieveConfiguration
{
    public class GetClientOrderReportByPromotionalIdSieveConfiguration : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<ClientOrder>(x => x.Id).CanSortAndFilterByName(nameof(GetClientOrderReportByPromotionalIdResponse.Id));
            mapper.Property<ClientOrder>(x => x.Number).CanSortAndFilterByName(nameof(GetClientOrderReportByPromotionalIdResponse.Number));
            mapper.Property<ClientOrder>(x => x.ClientOrderStatus).CanSortAndFilterByName(nameof(GetClientOrderReportByPromotionalIdResponse.ClientOrderStatus));
            mapper.Property<ClientOrder>(x => x.ClientId).CanSortAndFilterByName(nameof(GetClientOrderReportByPromotionalIdResponse.ClientId));
            mapper.Property<ClientOrder>(x => x.Rate).CanSortAndFilterByName(nameof(GetClientOrderReportByPromotionalIdResponse.Rate));
            mapper.Property<ClientOrder>(x => x.PromoCodeId).CanSortAndFilterByName(nameof(GetClientOrderReportByPromotionalIdResponse.PromoCodeId));
            mapper.Property<ClientOrder>(x => x.LocationId).CanSortAndFilterByName(nameof(GetClientOrderReportByPromotionalIdResponse.LocationId));
            mapper.Property<ClientOrder>(x => x.DriverFeeId).CanSortAndFilterByName(nameof(GetClientOrderReportByPromotionalIdResponse.DriverFeeId));
            mapper.Property<ClientOrder>(x => x.DriverClaimId).CanSortAndFilterByName(nameof(GetClientOrderReportByPromotionalIdResponse.DriverClaimId));
            mapper.Property<ClientOrder>(x => x.DeliveryTime).CanSortAndFilterByName(nameof(GetClientOrderReportByPromotionalIdResponse.DeliveryTime));
            mapper.Property<ClientOrder>(x => x.DeviceId).CanSortAndFilterByName(nameof(GetClientOrderReportByPromotionalIdResponse.DeviceId));
            mapper.Property<ClientOrder>(x => x.Tax).CanSortAndFilterByName(nameof(GetClientOrderReportByPromotionalIdResponse.Tax));
            mapper.Property<ClientOrder>(x => x.DeliveryFee).CanSortAndFilterByName(nameof(GetClientOrderReportByPromotionalIdResponse.DeliveryFee));
            mapper.Property<ClientOrder>(x => x.Cost).CanSortAndFilterByName(nameof(GetClientOrderReportByPromotionalIdResponse.Cost));
            mapper.Property<ClientOrder>(x => x.ServiceType).CanSortAndFilterByName(nameof(GetClientOrderReportByPromotionalIdResponse.ServiceType));
            mapper.Property<ClientOrder>(x => x.PromotionalLinkId).CanSortAndFilterByName(nameof(GetClientOrderReportByPromotionalIdResponse.PromotionalLinkId));
        }
    }
}
