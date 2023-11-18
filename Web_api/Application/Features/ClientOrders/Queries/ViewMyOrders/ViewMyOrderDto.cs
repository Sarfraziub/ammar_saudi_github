using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.Attribute;
using Domain.DbModel;
using EnumStringValues;

namespace Application.Features.ClientOrders.Queries.ViewMyOrders;

public class ViewMyOrderDto : IMapFrom<ClientOrder>
{
	public long Id { get; set; }
	public string Number { get; set; }
	public string Client { get; set; }
	public DateTime Created { get; set; }
	public ClientOrderStatuses ClientOrderStatus { get; set; }
	public string ClientOrderStatusName { get; set; }
	public string Driver { get; set; }

	public DateTime Updated { get; set; }

	public Rates? Rate { get; set; }
	public string Feedback { get; set; }
	public string LocationArabicName { get; set; }
	public string LocationName { get; set; }
	public string LocationArabicDescription { get; set; }

	public string LocationDescription { get; set; }

	public string DriverPhone { get; set; }
	public PaymentTypes? PaymentType { get; set; }
	public string PaymentTypeName { get; set; }
	[MultiCurrency]
	public decimal TotalAfterDiscount { get; set; }
	[MultiCurrency]
	public decimal Total { get; set; }
	[MultiCurrency]
	public decimal? Discount { get; set; }
	public string PromoCode { get; set; }
	public decimal? PromoCodePercentage { get; set; }
	public long LocationId { get; set; }
	[MultiCurrency]
	public decimal Tax { get; set; }
	[MultiCurrency]
	public decimal DeliveryFee { get; set; }
	[MultiCurrency]
	public decimal Cost { get; set; }
	public double Longitude { get; set; }
	public double Latitude { get; set; }
	public int OrderSalesItemQuantity { get; set; }
	public string CurrencyCode { get; set; }
	public void Mapping(Profile profile)
    {
        profile.CreateMap<ClientOrder, ViewMyOrderDto>()
            // .ForMember(d => d.Number,
            // 	opts =>
            // 		opts.MapFrom(s => s.Number))
            .ForMember(d => d.Client,
                opts =>
                    opts.MapFrom(s => !string.IsNullOrEmpty(s.Client.Name) ? s.Client.Name : s.Client.UserName))
            .ForMember(d => d.ClientOrderStatus,
                opts =>
                    opts.MapFrom(s => s.ClientOrderStatus.GetStringValue()))
            .ForMember(d => d.Driver,
                opts =>
                    opts.MapFrom(s => !string.IsNullOrEmpty(s.Driver.Name) ? s.Driver.Name : s.Driver.UserName))
            .ForMember(d => d.Total,
                opts =>
                    opts.MapFrom(s => s.ClientOrderDetails.Where(d => d.Active == 1).Select(f => f.Price * f.ClientOrder.ExchangeRate).Sum()))
            .ForMember(d => d.Updated,
                opts =>
                    opts.MapFrom(s => s.Updated ?? s.Created))
            .ForMember(d => d.LocationName,
                opts => opts.MapFrom(s => s.Location != null ? s.Location.Name : null))
            .ForMember(d => d.LocationArabicName,
                opts => opts.MapFrom(s => s.Location != null ? s.Location.ArabicName : null))
            .ForMember(d => d.ClientOrderStatus,
                opts => opts.MapFrom(s => s.ClientOrderStatus))
            .ForMember(d => d.ClientOrderStatusName,
                opts => opts.MapFrom(s => s.ClientOrderStatus.GetStringValue()))
            .ForMember(d => d.LocationDescription,
                opts => opts.MapFrom(
                    s => s.Location != null ? s.Location != null ? s.Location.Description : null : null))
            .ForMember(d => d.LocationArabicDescription,
                opts => opts.MapFrom(s =>
                    s.Location != null ? s.Location != null ? s.Location.ArabicDescription : null : null))
            .ForMember(d => d.LocationId,
                opts => opts.MapFrom(s => s.LocationId))
            .ForMember(d => d.DriverPhone,
                opts =>
                    opts.MapFrom(s => s.Driver != null ? s.Driver.UserName : null))
            // .ForMember(d => d.PaymentType,
            // 	opts =>
            // 		opts.MapFrom(s =>
            // 			s.PaymentTries != null && s.PaymentTries.Count > 0
            // 				? s.PaymentTries.OrderByDescending(d => d.Created).Take(1).Single().PaymentType
            // 				: (PaymentTypes?)null))
            // .ForMember(d => d.PaymentTypeName,
            // 	opts =>
            // 		opts.MapFrom(s =>
            // 			s.PaymentTries != null && s.PaymentTries.Count > 0
            // 				? s.PaymentTries.OrderByDescending(d => d.Created).Take(1).Single().PaymentType
            // 					.GetStringValue()
            // 				: null))
            .ForMember(d => d.OrderSalesItemQuantity,
                opts =>
                    opts.MapFrom(s =>
                        s.ClientOrderDetails.Where(f => f.Active == 1)
                            .Sum(dd => dd.Quantity * dd.SaleItem.SalesItemQuantity)))
            .ForMember(d => d.Total,
                opts =>
                    opts.MapFrom(s =>
                        s.ClientOrderDetails.Where(f => f.Active == 1).Sum(dd => (dd.Price * dd.ClientOrder.ExchangeRate) * dd.Quantity)))
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
                            ? s.ClientOrderDetails.Where(d => d.Active == 1).Sum(f => (f.Price * f.ClientOrder.ExchangeRate) * f.Quantity) -
                              s.ClientOrderDetails.Where(d => d.Active == 1).Sum(f => (f.Price * f.ClientOrder.ExchangeRate) * f.Quantity) *
                              s.PromoCode.Percentage
                            : s.ClientOrderDetails.Where(d => d.Active == 1).Sum(f => (f.Price * f.ClientOrder.ExchangeRate) * f.Quantity))
            )
            .ForMember(d => d.LocationUrl,
                opts => opts.MapFrom(s => s.Location != null ? s.Location.Url : null))


            .ForMember(d => d.Longitude,
                opts => opts.MapFrom(s => s.Location != null ? s.Location.Longitude : (double?)null))

            .ForMember(d => d.Latitude,
                opts => opts.MapFrom(s => s.Location != null ? s.Location.Latitude : (double?)null))
            .ForMember(d => d.Tax,
                opts => opts.MapFrom(s => s.Tax * s.ExchangeRate))
            .ForMember(d => d.DeliveryFee,
                opts => opts.MapFrom(s => s.DeliveryFee * s.ExchangeRate))
            .ForMember(d => d.Cost,
                opts => opts.MapFrom(s => s.ChargedPrice))
            .ForMember(d => d.CurrencyCode,
                opts => opts.MapFrom(s => s.ChargedCurrency.Code));
    }

	public string LocationUrl { get; set; }
}

