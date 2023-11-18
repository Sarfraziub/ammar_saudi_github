
namespace Domain.DbModel
{
    public class Currency : Entity
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public ICollection<ExchangeRate> FromCurrencyExchangeRate { get; set; }
        public ICollection<ExchangeRate> ToCurrencyExchangeRate { get; set; }
    }
}
