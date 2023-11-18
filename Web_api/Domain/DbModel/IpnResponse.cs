namespace Domain.DbModel;

public class IpnResponse : Entity
{
    public PaymentTry PaymentTry { get; set; }

    public string Response { get; set; }
    public long PaymentTryId { get; set; }
    public int MerchantId { get; set; }
    public int ProfileId { get; set; }

    public string TransactionReference { get; set; }
    public string TransactionType { get; set; }

    public float CartAmount { get; set; }
    public string CartCurrency { get; set; }
    public string CartId { get; set; }
    public string CartDescription { get; set; }

    public string TransactionClass { get; set; }
    public string TransactionCurrency { get; set; }
    public float TransactionTotal { get; set; }

    public string ResponseStatus { get; set; }
    public string ResponseCode { get; set; }
    public string ResponseMessage { get; set; }

    public string CvvResult { get; set; }
    public string AvsResult { get; set; }

    public DateTime TransactionTime { get; set; }
}


