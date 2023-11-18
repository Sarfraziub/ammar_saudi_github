using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.Attribute;
using Domain.DbModel;
using EnumStringValues;

namespace Application.Features.ClientOrders.Queries.GetClientOrderDetailsById;

public class GetClientOrderDetailsByIdDto : IMapFrom<ClientOrderDetail>
{
	public long Id { get; set; }

	public string Number { get; set; }

	// public string ClientOrderStatusName { get; set; }
	public string SaleItemName { get; set; }
	public string SaleItemArabicName { get; set; }
	public string SaleItemSpecifications { get; set; }
	public string SaleItemArabicSpecifications { get; set; }
	[MultiCurrency]
	public decimal SaleItemPrice { get; set; }
	public long SaleItemImageId { get; set; }
	public string SaleItemImageUrl { get; set; }
	public int SaleItemQuantity { get; set; }
	public string LocationName { get; set; }
	public string LocationArabicName { get; set; }

	public DateTime Created { get; set; }

	public DateTime? Updated { get; set; }

	public string Driver { get; set; }
	public ClientOrderStatuses ClientOrderStatus { get; set; }
	public string ClientOrderStatusName { get; set; }
	public string LocationUrl { get; set; }

	public double Longitude { get; set; }
	public double Latitude { get; set; }
	[MultiCurrency]
	public decimal Tax { get; set; }
	[MultiCurrency]
    public decimal DeliveryFee { get; set; }
	public ServiceTypes? ServiceType { get; set; }
	[MultiCurrency]
    public decimal Cost { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<ClientOrderDetail, GetClientOrderDetailsByIdDto>()
			.ForMember(d => d.Number,
				opts => opts.MapFrom(s => s.ClientOrder.Number))
			.ForMember(d => d.SaleItemName,
				opts => opts.MapFrom(s => s.SaleItem.Name))
			.ForMember(d => d.SaleItemArabicName,
				opts => opts.MapFrom(s => s.SaleItem.ArabicName))
			.ForMember(d => d.SaleItemSpecifications,
				opts => opts.MapFrom(s => s.SaleItem.Specifications))
			.ForMember(d => d.SaleItemArabicSpecifications,
				opts => opts.MapFrom(s => s.SaleItem.ArabicSpecifications))
			.ForMember(d => d.SaleItemPrice,
				opts => opts.MapFrom(s => s.Price))
			.ForMember(d => d.SaleItemImageId,
				opts => opts.MapFrom(s => s.SaleItem.ImageId))
			.ForMember(d => d.SaleItemQuantity,
				opts => opts.MapFrom(s => s.Quantity))
			.ForMember(d => d.LocationName,
				opts => opts.MapFrom(s => s.ClientOrder.Location != null ? s.ClientOrder.Location.Name : null))
			.ForMember(d => d.LocationArabicName,
				opts => opts.MapFrom(s => s.ClientOrder.Location != null ? s.ClientOrder.Location.ArabicName : null))
			.ForMember(d => d.Driver,
				opts => opts.MapFrom(s => s.ClientOrder.Driver != null ? s.ClientOrder.Driver.Name : null))
			.ForMember(d => d.ClientOrderStatus,
				opts => opts.MapFrom(s => s.ClientOrder.ClientOrderStatus))
			.ForMember(d => d.ClientOrderStatusName,
				opts => opts.MapFrom(s => s.ClientOrder.ClientOrderStatus.GetStringValue()))
			.ForMember(d => d.Created,
				opts =>
					opts.MapFrom(s => s.ClientOrder.Updated ?? s.ClientOrder.Created))
			.ForMember(d => d.Updated,
				opts =>
					opts.MapFrom(s => s.ClientOrder.Updated))

			.ForMember(d => d.LocationUrl,
				opts => opts.MapFrom(s => s.ClientOrder.Location != null ? s.ClientOrder.Location.Url : null))

			.ForMember(d => d.Latitude,
				opts => opts.MapFrom(s => s.ClientOrder.Location != null ? s.ClientOrder.Location.Latitude : (double?)null))

			.ForMember(d => d.Longitude,
				opts => opts.MapFrom(s => s.ClientOrder.Location != null ? s.ClientOrder.Location.Longitude : (double?)null))

			.ForMember(d => d.Cost,
				opts => opts.MapFrom(s => s.ClientOrder.Cost))

			.ForMember(d => d.ServiceType,
				opts => opts.MapFrom(s => s.ClientOrder.ServiceType))

			;
	}
}


