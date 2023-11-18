using Domain.Attribute;
using static Domain.DbModel.Entity;

namespace Domain.DbModel;

public class ClientOrder : Entity, IMultiCurrency
{
	public string Number { get; set; }
	public ClientOrderStatuses ClientOrderStatus { get; set; }
	
	public long? DriverId { get; set; }
	
	public long? ClientId { get; set; }

	public Rates? Rate { get; set; }
	public string Feedback { get; set; }
	public bool? HideFeedback { get; set; }

	public long? PromoCodeId { get; set; }
	public string? PromoCodeAppliedSource { get; set; }
    public long? LocationId { get; set; }
    public long? DriverFeeId { get; set; }
    public long? DriverClaimId { get; set; }
	public DateTime? DeliveryTime { get; set; }
    public string DeviceId { get; set; }
	public decimal Tax { get; set; }
	public decimal DeliveryFee { get; set; }
    public decimal Cost { get; set; }
    public ServiceTypes? ServiceType { get; set; }
	public long? PromotionalLinkId { get; set; }
	public string? DeviceSource { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? DeviceToken { get; set; }
    public long ChargedCurrencyId { get; set; }
    public decimal ChargedPrice { get; set; }
    public decimal ExchangeRate { get; set; }
    public DateTime? DriverAssignedOn { get; set; }
    public bool? FlgSelected { get; set; }
    public PromotionalLink PromotionalLink { get; set; }
    public ICollection<ClientOrderPayment> ClientOrderPayments { get; set; }
    public ICollection<ClientOrderDeliverImage> ClientOrderDeliverImages { get; set; }
    public Currency ChargedCurrency { get; set; }
    public virtual ICollection<Gift> Gift { get; set; }
    public DriverFee DriverFee { get; set; }
    public DriverClaim DriverClaim { get; set; }
    public ICollection<PaymentTry> PaymentTries { get; set; }
    public Location Location { get; set; }
    public PromoCode PromoCode { get; set; }
    public ICollection<ClientOrderDetail> ClientOrderDetails { get; set; }
    public ICollection<ClientOrderLog> ClientOrderLogs { get; set; }
    public ApplicationUser Client { get; set; }
    public ApplicationUser Driver { get; set; }
}