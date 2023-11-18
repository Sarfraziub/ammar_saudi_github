using Application.Features.Common.Interfaces;

namespace Infrastructures;

public class MachineDateTime : IDateTime
{
	public DateTime Now => DateTime.Now;
	public DateTime UtcNow => DateTime.UtcNow;
	public string Format => "dd/MM/yyyy hh:mm";

	public DateTime EndOfDay(DateTime date)
	{
		return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
	}

	public DateTime StartOfDay(DateTime date)
	{
		return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
	}

	public DateTime StartOfWeek()
	{
		return DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Sunday);
	}
}
