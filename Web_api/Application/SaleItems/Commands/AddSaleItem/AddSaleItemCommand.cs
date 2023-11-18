using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.SaleItems.Commands.AddSaleItem;

public class AddSaleItemCommand : IRequest<Unit>, IMapFrom<SaleItem>
{
	public string Name { get; set; }
	public string ArabicName { get; set; }
	public string Specifications { get; set; }
	public string ArabicSpecifications { get; set; }
	public double Price { get; set; }
	public long ImageId { get; set; }
	public long? PackageId { get; set; }
    public int SalesItemQuantity { get; set; }

    public void Mapping(Profile profile)
	{
		profile.CreateMap<AddSaleItemCommand, SaleItem>();
	}
}


