using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;

namespace Application.SaleItemPackages.Queries.GetPackageById;

public class GetPackageByIdDto : IMapFrom<Package>
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }

	public string ArabicName { get; set; }
	public string ArabicDescription { get; set; }

	public bool Visible { get; set; }
	public string ImageUrl { get; set; }
	public long FileId { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<Package, GetPackageByIdDto>();
	}
}


