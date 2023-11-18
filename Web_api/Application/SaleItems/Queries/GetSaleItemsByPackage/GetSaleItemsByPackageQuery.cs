using MediatR;

namespace Application.SaleItems.Queries.GetSaleItemsByPackage;

public class GetSaleItemsByPackageQuery : IRequest<GetSaleItemsByPackageVm>
{
	public long PackageId { get; set; }
	public string DeviceId { get; set; }
}


