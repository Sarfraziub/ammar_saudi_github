using MediatR;

namespace Application.SaleItemPackages.Commands.UpdatePackage;

public class UpdatePackageCommand : IRequest<Unit>
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }

	public string ArabicName { get; set; }
	public string ArabicDescription { get; set; }
	public bool Visible { get; set; }
	public long? FileId { get; set; }

}
