using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.SaleItemPackages.Commands.AddPackage;

public class AddPackageCommand : IRequest<Unit>, IMapFrom<Package>
{
	public string Name { get; set; }
	public string Description { get; set; }

	public string ArabicName { get; set; }
	public string ArabicDescription { get; set; }
	public bool Visible { get; set; }
	public long? FileId { get; set; }


	public void Mapping(Profile profile)
	{
		profile.CreateMap<AddPackageCommand, Package>()
			;
	}
}
