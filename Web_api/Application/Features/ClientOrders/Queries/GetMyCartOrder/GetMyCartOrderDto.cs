using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.Attribute;
using Domain.DbModel;
using EnumStringValues;

namespace Application.Features.ClientOrders.Queries.GetMyCartOrder;

public class GetMyCartOrderDto : IMapFrom<ClientOrder>
{
    public long Id { get; set; }
    public string Number { get; set; }
    public long LocationId { get; set; }
    public string LocationName { get; set; }
    public string LocationArabicName { get; set; }

    public string LocationDescription { get; set; }
    public string LocationArabicDescription { get; set; }
    public string Driver { get; set; }
    public ClientOrderStatuses ClientOrderStatus { get; set; }
    public string ClientOrderStatusName { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    [MultiCurrency]
    public decimal TotalAfterDiscount { get; set; }
    [MultiCurrency]
    public decimal Total { get; set; }
    [MultiCurrency]
    public decimal? Discount { get; set; }
    public string PromoCode { get; set; }
    public decimal? PromoCodePercentage { get; set; }
    [MultiCurrency]
    public decimal Tax { get; set; }
    [MultiCurrency]
    public decimal DeliveryFee { get; set; }

    [MultiCurrency]
    public decimal FinalTotal { get; set; }
    public decimal FinalTotalDefaultCurrency { get; set; }
    public LocationTypes LocationType { get; set; }
    public ServiceTypes? ServiceType { get; set; }
    public int OrderSalesItemQuantity { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClientOrder, GetMyCartOrderDto>()
            .ForMember(d => d.LocationId,
                opts => opts.MapFrom(s => s.LocationId))
            .ForMember(d => d.LocationName,
                opts => opts.MapFrom(s => s.Location != null ? s.Location.Name : null))
            .ForMember(d => d.LocationArabicName,
                opts => opts.MapFrom(s => s.Location != null ? s.Location.ArabicName : null))
            .ForMember(d => d.LocationDescription,
                opts => opts.MapFrom(s => s.Location != null ? s.Location.Description : null))
            .ForMember(d => d.LocationArabicDescription,
                opts => opts.MapFrom(s => s.Location != null ? s.Location.ArabicDescription : null))
            .ForMember(d => d.Driver,
                opts => opts.MapFrom(s => s.Driver != null ? s.Driver.Name : null))
            .ForMember(d => d.ClientOrderStatus,
                opts => opts.MapFrom(s => s.ClientOrderStatus))
            .ForMember(d => d.ClientOrderStatusName,
                opts => opts.MapFrom(s => s.ClientOrderStatus.GetStringValue()))
            .ForMember(d => d.Created,
                opts =>
                    opts.MapFrom(s => s.Updated ?? s.Created))
            .ForMember(d => d.Updated,
                opts =>
                    opts.MapFrom(s => s.Updated ?? s.Created))
            .ForMember(d => d.Discount,
                opts =>
                    opts.MapFrom(s =>
                        s.PromoCode != null
                            ? s.ClientOrderDetails.Where(d => d.Active == 1).Sum(f => f.Price * f.Quantity) *
                              s.PromoCode.Percentage
                            : 0
                    )
            )
            .ForMember(d => d.OrderSalesItemQuantity,
                opts =>
                    opts.MapFrom(s => s.ClientOrderDetails.Where(f => f.Active == 1).Sum(dd => dd.Quantity * dd.SaleItem.SalesItemQuantity)))
            .ForMember(d => d.Total,
                opts =>
                    opts.MapFrom(s =>
                        s.ClientOrderDetails.Where(f => f.Active == 1).Sum(dd => dd.Price * dd.Quantity)))
            .ForMember(d => d.PromoCode,
                opts =>
                    opts.MapFrom(s => s.PromoCode != null ? s.PromoCode.Code : null))
            .ForMember(d => d.PromoCodePercentage,
                opts =>
                    opts.MapFrom(s => s.PromoCode != null ? s.PromoCode.Percentage : 0))
            .ForMember(d => d.TotalAfterDiscount,
                opts =>
                    opts.MapFrom(s =>
                        s.PromoCode != null
                            ? s.ClientOrderDetails.Where(d => d.Active == 1).Sum(f => f.Price * f.Quantity) -
                              s.ClientOrderDetails.Where(d => d.Active == 1).Sum(f => f.Price * f.Quantity) *
                              s.PromoCode.Percentage
                            : s.ClientOrderDetails.Where(d => d.Active == 1).Sum(f => f.Price * f.Quantity))
            )
            .ForMember(d => d.LocationUrl,
                opts => opts.MapFrom(s => s.Location.Url))

            .ForMember(d => d.LocationType,
                opts => opts.MapFrom(s => s.Location != null ? s.Location.LocationType : (LocationTypes?)null))


            ;
    }

    public string LocationUrl { get; set; }
}


