using EnumStringValues;

namespace Domain.DbModel;

public enum ClientOrderStatuses
{
	[StringValue("جديد")] New = 1,
	[StringValue("تم استلام الدفعة")] PaymentReceived = 2,
	[StringValue("مع سائق")] WithDriver = 3,
	[StringValue("تم التوصيل")] Delivered = 4
}

