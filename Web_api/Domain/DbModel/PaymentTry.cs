namespace Domain.DbModel;

public class PaymentTry : Entity
{
    public ClientOrder ClientOrder { get; set; }
    public long ClientOrderId { get; set; }
    public int ProfileId { get; set; }
    public string TransactionType { get; set; }
    public string TransactionClass { get; set; }
    public string CartDescription { get; set; }
    public string OrderReferenceId { get; set; }
    public string Currency { get; set; }
    public decimal Amount { get; set; }
    public string Return { get; set; }
    public string Callback { get; set; }


    public string TransactionReference { get; set; }
    public string CartId { get; set; }
    public string RedirectUrl { get; set; }
    public string Trace { get; set; }
    public string Message { get; set; }
    public ICollection<PaymentResponse> PaymentResponses { get; set; }
    public ICollection<IpnResponse> IpnResponses { get; set; }

    public PaymentTypes PaymentType { get; set; }
}


