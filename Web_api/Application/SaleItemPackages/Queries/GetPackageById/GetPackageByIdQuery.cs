using MediatR;

namespace Application.SaleItemPackages.Queries.GetPackageById;

public class GetPackageByIdQuery : IRequest<GetPackageByIdDto>
{
	public long Id { get; set; }
}


