using EnumStringValues;

namespace Domain.DbModel;

public enum PaymentTypes
{
	[StringValue("Visa")] Visa = 1,
	[StringValue("Mastercard")] Mastercard = 2,
	[StringValue("ApplyPay")] ApplyPay = 3,
	[StringValue("Mada")] Mada = 4
}


