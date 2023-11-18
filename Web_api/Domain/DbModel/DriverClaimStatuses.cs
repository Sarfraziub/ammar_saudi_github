using EnumStringValues;

namespace Domain.DbModel;

public enum DriverClaimStatuses
{
	[StringValue("قيد الانتظار")] Pending = 1,
	[StringValue("مكتمل")] Completed = 2
}
