
namespace Domain.DbModel
{
    public class ExchangeRate : Entity
    {
        public long FromCurrencyId { get; set; }
        public long ToCurrencyId { get; set; }
        public decimal Rate { get; set; }

        public Currency FromCurrency { get; set; }
        public Currency ToCurrency { get; set; }
    }
}
