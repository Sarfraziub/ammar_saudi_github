using EnumStringValues;

namespace Domain.DbModel;

public enum ClientOrderActionLogStatuses
{
	[StringValue("CreateOrder")] CreateOrder = 1,
	[StringValue("AddSaleItem")] AddSaleItem = 2,
	[StringValue("RequestPaymentPage")] RequestPaymentPage = 3,

	[StringValue("RequestPaymentPageReceived")]
	RequestPaymentPageReceived = 4,
	[StringValue("OrderPaid")] OrderPaid = 5,
	[StringValue("OrderNotPaid")] OrderNotPaid = 6,
	[StringValue("AssignDriver")] AssignDriver = 7,
	[StringValue("AssignDriver")] AssignLocation = 8,
	[StringValue("CancelOrder")] CancelOrder = 9,
	[StringValue("ChangeDriver")] OrderDelivered = 10,
	[StringValue("GiveFeedback")] GiveFeedback = 11,
	[StringValue("AssignDriver")] UnassignDriver = 12,
	[StringValue("DriverAcceptOrder")] DriverAcceptOrder = 13,
	[StringValue("DriverRejectOrder")] DriverRejectOrder = 14,
	[StringValue("DriverRejectOrder")] DriverRequestClaim = 15,
	[StringValue("DriverRejectOrder")] DriverCancelClaim = 16,
	[StringValue("AssignDriver")] ChangeLocation = 17,
	[StringValue("TransferFromGuestUser")] TransferFromGuestUser = 18,
}
