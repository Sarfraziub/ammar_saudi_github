using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;
using EnumStringValues;

namespace Application.Features.ClientOrders.Queries.GetClientOrdersByClientId;

public class GetClientOrdersByClientIdDto : IMapFrom<ClientOrder>
{
	public long Id { get; set; }
	public string Number { get; set; }
	public string Client { get; set; }
	public DateTime Created { get; set; }
	public ClientOrderStatuses ClientOrderStatus { get; set; }
	public string ClientOrderStatusName { get; set; }
	public string Driver { get; set; }

	public DateTime Updated { get; set; }

	public long LocationId { get; set; }
	public Rates? Rate { get; set; }
	public string Feedback { get; set; }
	public string LocationArabicName { get; set; }
	public string LocationName { get; set; }
	public string LocationArabicDescription { get; set; }

	public string LocationDescription { get; set; }


	public decimal TotalAfterDiscount { get; set; }
	public decimal Total { get; set; }
	public decimal? Discount { get; set; }
	public string PromoCode { get; set; }
	public decimal? PromoCodePercentage { get; set; }
	public decimal Tax { get; set; }
	public decimal DeliveryFee { get; set; }
	public decimal Cost { get; set; }
    public int OrderSalesItemQuantity { get; set; }

    public void Mapping(Profile profile)
	{
		profile.CreateMap<ClientOrder, GetClientOrdersByClientIdDto>()
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
					opts.MapFrom(s => s.ClientOrderDetails.Where(d => d.Active == 1).Select(f => f.Price).Sum()))
			.ForMember(d => d.Discount,
				opts =>
					opts.MapFrom(s =>
						s.PromoCode != null
							? s.ClientOrderDetails.Where(d => d.Active == 1).Select(f => f.Price).Sum() *
							  s.PromoCode.Percentage
							: 0
					)
			)
			.ForMember(d => d.Updated,
				opts =>
					opts.MapFrom(s => s.Updated ?? s.Created))
			.ForMember(d => d.LocationName,
				opts => opts.MapFrom(s => s.Location.Name))
			.ForMember(d => d.LocationArabicName,
				opts => opts.MapFrom(s => s.Location.ArabicName))
			.ForMember(d => d.ClientOrderStatus,
				opts => opts.MapFrom(s => s.ClientOrderStatus))
			.ForMember(d => d.ClientOrderStatusName,
				opts => opts.MapFrom(s => s.ClientOrderStatus.GetStringValue()))
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

			;
	}

	public string LocationUrl { get; set; }
}


