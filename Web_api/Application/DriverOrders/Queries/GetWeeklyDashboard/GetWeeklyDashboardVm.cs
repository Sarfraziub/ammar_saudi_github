namespace Application.DriverOrders.Queries.GetWeeklyDashboard;

public class GetWeeklyDashboardVm
{
	public int Trips { get; set; }
	public decimal Unpaid { get; set; }

	// public DayOfWeek Sunday { get; set; }
	public decimal SundayIncome { get; set; }

	// public DayOfWeek Monday { get; set; }
	public decimal MondayIncome { get; set; }

	// public DayOfWeek Tusday { get; set; }
	public decimal TuesdayIncome { get; set; }

	// public DayOfWeek Wednesday { get; set; }
	public decimal WednesdayIncome { get; set; }

	// public DayOfWeek Thursday { get; set; }
	public decimal ThursdayIncome { get; set; }

	// public DayOfWeek Friday { get; set; }
	public decimal FridayIncome { get; set; }
	public decimal SaturdayIncome { get; set; }


	public List<GetWeeklyDashboardUnpaidDto> ClientOrders { get; set; }

}
