using MediatR;

namespace Application.SaleItemPackages.Queries.GetPackages;

public class GetPackagesQuery : IRequest<GetPackagesVm>
{
	public bool? Visible { get; set; }
}
