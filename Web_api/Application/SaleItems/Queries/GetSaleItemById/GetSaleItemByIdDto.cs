using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;

namespace Application.SaleItems.Queries.GetSaleItemById;

public class GetSaleItemByIdDto : IMapFrom<SaleItem>
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string ArabicName { get; set; }
	public string Specifications { get; set; }
	public string ArabicSpecifications { get; set; }
	public decimal Price { get; set; }
	public long ImageId { get; set; }
	public string ImageUrl { get; set; }

	public long? PackageId { get; set; }
	public string PackageName { get; set; }
	public string PackageDescription { get; set; }

	public string PackageArabicName { get; set; }
	public string PackageArabicDescription { get; set; }
    public int SalesItemQuantity { get; set; }

    public void Mapping(Profile profile)
	{
		profile.CreateMap<SaleItem, GetSaleItemByIdDto>()
			.ForMember(d => d.PackageName, opts => opts.MapFrom(s => s.Package != null ? s.Package.Name : null))
			.ForMember(d => d.PackageDescription, opts => opts.MapFrom(s => s.Package != null ? s.Package.Description : null))
			.ForMember(d => d.PackageArabicName, opts => opts.MapFrom(s => s.Package != null ? s.Package.ArabicName : null))
			.ForMember(d => d.PackageArabicDescription, opts => opts.MapFrom(s => s.Package != null ? s.Package.ArabicDescription : null))
			;
	}
}

