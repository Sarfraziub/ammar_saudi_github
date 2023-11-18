using MediatR;

namespace Application.Drivers.Queries.GetDrivers;

public class GetDriversQuery : IRequest<GetDriversVm>
{
	public bool? ActiveDriver { get; set; }
}
