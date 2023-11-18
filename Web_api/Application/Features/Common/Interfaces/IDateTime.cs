namespace Application.Features.Common.Interfaces;

public interface IDateTime
{
	DateTime Now { get; }
	DateTime UtcNow { get; }
	string Format { get; }
	DateTime EndOfDay(DateTime date);
	DateTime StartOfDay(DateTime date);
	DateTime StartOfWeek();

}


