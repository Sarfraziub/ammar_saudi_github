using Application.Features.Common.Mappings;
using AutoMapper;
using Domain.DbModel;
using EnumStringValues;

namespace Application.Features.ClientOrders.Queries.GetClientOrderById;

public class GetClientOrderByIdDto : IMapFrom<ClientOrder>
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
	public decimal TotalAfterDiscount { get; set; }
	public decimal Total { get; set; }
	public decimal? Discount { get; set; }
	public string PromoCode { get; set; }
	public decimal? PromoCodePercentage { get; set; }
	public string LocationUrl { get; set; }
	public double Longitude { get; set; }
	public double Latitude { get; set; }
	public decimal Tax { get; set; }
	public decimal DeliveryFee { get; set; }
	public decimal Cost { get; set; }
	public decimal Fee { get; set; }
	public FeeTypes? FeeType { get; set; }
	public decimal FeeValue { get; set; }
	public int OrderSalesItemQuantity { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<ClientOrder, GetClientOrderByIdDto>()
			.ForMember(d => d.LocationId,
				opts => opts.MapFrom(s => s.LocationId))
			.ForMember(d => d.LocationName,
				opts => opts.MapFrom(s => s.Location.Name))
			.ForMember(d => d.LocationArabicName,
				opts => opts.MapFrom(s => s.Location.ArabicName))
			.ForMember(d => d.LocationDescription,
				opts => opts.MapFrom(s => s.Location.Description))
			.ForMember(d => d.LocationArabicDescription,
				opts => opts.MapFrom(s => s.Location.ArabicDescription))
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
				opts => opts.MapFrom(s => s.Location != null ? s.Location.Url : null))

			.ForMember(d => d.Longitude,
				opts => opts.MapFrom(s => s.Location != null ? s.Location.Longitude : (double?)null))

			.ForMember(d => d.Latitude,
				opts => opts.MapFrom(s => s.Location != null ? s.Location.Latitude : (double?)null))


			.ForMember(d => d.FeeType,
				opts => opts.MapFrom(s => s.DriverFee != null ? s.DriverFee.FeeType : (FeeTypes?)null))


			.ForMember(d => d.FeeValue,
				opts => opts.MapFrom(s =>  s.DriverFee != null ? s.DriverFee.Value : (decimal?)null))

			;
	}
}


