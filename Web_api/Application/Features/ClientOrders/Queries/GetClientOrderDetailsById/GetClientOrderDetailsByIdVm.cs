namespace Application.Features.ClientOrders.Queries.GetClientOrderDetailsById;

public class GetClientOrderDetailsByIdVm
{
	public string Number { get; set; }
	public List<GetClientOrderDetailsByIdDto> ClientOrderDetails { get; set; }
	public decimal Total { get; set; }
}


