using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;

namespace Application.Features.ClientOrders.Queries.GetClientOrderReportByPromotionalId
{
    public class GetClientOrderReportByPromotionalIdResponse : IMapFrom<ClientOrder>
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public ClientOrderStatuses ClientOrderStatus { get; set; }
        public long? DriverId { get; set; }
        
        public long? ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientMobileNo { get; set; }
        public string ClientEmail { get; set; }

        public Rates? Rate { get; set; }

        public long? PromoCodeId { get; set; }

        public long? LocationId { get; set; }
        public long? DriverFeeId { get; set; }

        public long? DriverClaimId { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public string DeviceId { get; set; }
        public decimal Tax { get; set; }
        public decimal DeliveryFee { get; set; }

        public decimal Cost { get; set; }
        public ServiceTypes? ServiceType { get; set; }
        public long? PromotionalLinkId { get; set; }
        public DateTime Created { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ClientOrder, GetClientOrderReportByPromotionalIdResponse>()
                .ForMember(
                d => d.ClientName,
                opt =>
                    opt.MapFrom(s =>
                        s.Client.Name))
                .ForMember(
                    d => d.ClientMobileNo,
                    opt =>
                        opt.MapFrom(s =>
                            s.Client.PhoneNumber))
                .ForMember(
                    d => d.ClientEmail,
                    opt =>
                        opt.MapFrom(s =>
                            s.Client.Email));
        }
    }
}
