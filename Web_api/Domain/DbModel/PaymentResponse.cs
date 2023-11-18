namespace Domain.DbModel;

public class PaymentResponse : Entity
{
    public PaymentTry PaymentTry { get; set; }
    public long PaymentTryId { get; set; }
    public string TransactionReference { get; set; }
    public string ResponseCode { get; set; }
    public string ResponseMessage { get; set; }
    public string ResponseStatus { get; set; }
    public string AcquirerMessage { get; set; }
    public string AcquirerRrn { get; set; }
    public string CartId { get; set; }
    public string CustomerEmail { get; set; }
    public string Signature { get; set; }
    public string Token { get; set; }
}


