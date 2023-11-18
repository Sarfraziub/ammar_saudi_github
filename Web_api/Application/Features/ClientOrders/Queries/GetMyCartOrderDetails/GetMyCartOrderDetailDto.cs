using Application.Features.Common.Mappings;
using AutoMapper;
using Domain.Attribute;
using Domain.DbModel;

namespace Application.Features.ClientOrders.Queries.GetMyCartOrderDetails;

public class GetMyCartOrderDetailDto : IMapFrom<ClientOrderDetail>
{
	public long Id { get; set; }
	public long SaleItemId { get; set; }
	public string SaleItemName { get; set; }
	public string SaleItemArabicName { get; set; }
	public string SaleItemSpecifications { get; set; }
	public string SaleItemArabicSpecifications { get; set; }
	[MultiCurrency]
	public decimal SaleItemPrice { get; set; }
	public long SaleItemImageId { get; set; }
	public string SaleItemImageUrl { get; set; }
	public int SaleItemQuantity { get; set; }
	public long PackageId { get; set; }
	public string CurrencyCode { get; set; }


	public void Mapping(Profile profile)
	{
		profile.CreateMap<ClientOrderDetail, GetMyCartOrderDetailDto>()
			.ForMember(d => d.SaleItemId,
				opts => opts.MapFrom(s => s.SaleItemId))
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
			.ForMember(d => d.PackageId,
				opts => opts.MapFrom(s => s.SaleItem.PackageId))

			;
	}
}


