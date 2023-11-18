using MediatR;

namespace Application.SaleItemPackages.Commands.DeletePackage;

public class DeletePackageCommand : IRequest<Unit>
{
	public long Id { get; set; }
}
