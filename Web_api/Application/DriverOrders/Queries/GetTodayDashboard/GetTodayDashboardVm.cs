namespace Application.DriverOrders.Queries.GetTodayDashboard;

public class GetTodayDashboardVm
{
	public int Trips { get; set; }
	public decimal Unpaid { get; set; }
	public List<GetTodayDashboardUnpaidDto> ClientOrders { get; set; }

}