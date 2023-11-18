using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.Attribute;
using Domain.DbModel;
using EnumStringValues;

namespace Application.Features.ClientOrders.Queries.ViewClientOrders;

public class ViewClientOrderDto : IMapFrom<ClientOrder>
{
	public long Id { get; set; }
	public string Number { get; set; }
	public string Client { get; set; }
	public DateTime Created { get; set; }
	public ClientOrderStatuses ClientOrderStatus { get; set; }
	public string ClientOrderStatusName { get; set; }
	public string Driver { get; set; }

	// public long? DriverId { get; set; }
	public DateTime Updated { get; set; }

	public Rates? Rate { get; set; }
	public string Feedback { get; set; }

	public long LocationId { get; set; }
	public string LocationName { get; set; }
	public string LocationArabicName { get; set; }

	public string LocationDescription { get; set; }
	public string LocationArabicDescription { get; set; }

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
	public decimal Cost { get; set; }
	public bool AllowAssignDriver { get; set; }
	public string LocationUrl { get; set; }

	public double Longitude { get; set; }
	public double Latitude { get; set; }
	public bool AllowAssignLocation { get; set; }
	public int OrderSalesItemQuantity { get; set; }
    public DateTime? PaymentDate { get; set; }
    public DateTime? DriverAssignedOn { get; set; }
    public string UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? DeliveryTime { get; set; }


    public void Mapping(Profile profile)
	{
		profile.CreateMap<ClientOrder, ViewClientOrderDto>()
			// .ForMember(d => d.Number,
			// 	opts =>
			// 		opts.MapFrom(s => s.Number))
			.ForMember(d => d.Client,
				opts =>
					opts.MapFrom(s => !string.IsNullOrEmpty(s.Client.Name) ? s.Client.Name : s.Client.UserName))
			.ForMember(d => d.ClientOrderStatus,
				opts => opts.MapFrom(s => s.ClientOrderStatus))
			.ForMember(d => d.ClientOrderStatusName,
				opts => opts.MapFrom(s => s.ClientOrderStatus.GetStringValue()))
			.ForMember(d => d.Driver,
				opts =>
					opts.MapFrom(s => !string.IsNullOrEmpty(s.Driver.Name) ? s.Driver.Name : s.Driver.UserName))
			.ForMember(d => d.Updated,
				opts =>
					opts.MapFrom(s => s.Updated ?? s.Created))
			.ForMember(d => d.LocationName,
				opts => opts.MapFrom(s => s.Location.Name))
			.ForMember(d => d.LocationArabicName,
				opts => opts.MapFrom(s => s.Location.ArabicName))
			.ForMember(d => d.LocationDescription,
				opts => opts.MapFrom(s => s.Location.Description))
			.ForMember(d => d.LocationArabicDescription,
				opts => opts.MapFrom(s => s.Location.ArabicDescription))
			.ForMember(d => d.LocationId,
				opts => opts.MapFrom(s => s.LocationId))
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


			.ForMember(d => d.Latitude,
				opts => opts.MapFrom(s => s.Location != null ? s.Location.Latitude : (double?)null))

			.ForMember(d => d.Longitude,
				opts => opts.MapFrom(s => s.Location != null ? s.Location.Longitude : (double?)null))


			.ForMember(d => d.AllowAssignDriver,
				opts => opts.MapFrom(s => s.ClientOrderStatus == ClientOrderStatuses.PaymentReceived))


			.ForMember(d => d.AllowAssignLocation,
				opts => opts.MapFrom(s => s.ClientOrderStatus == ClientOrderStatuses.PaymentReceived && s.LocationId == null))
            .ForMember(d => d.UserName,
                opts => opts.MapFrom(s => s.Client != null ? s.Client.UserName : null))
            .ForMember(d => d.PhoneNumber,
                opts => opts.MapFrom(s => s.Client != null ? s.Client.PhoneNumber : null))
            ;
    }



}


