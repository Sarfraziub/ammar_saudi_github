
using Domain.DbModel;

namespace Application.Interface
{
    public interface ICurrencyService
    {
        Task<List<T>> ConvertToCurrencyValue<T>(long fromCurrencyId, long toCurrencyId, List<T> models);
        Task<T> ConvertToCurrencyValue<T>(long fromCurrencyId, long toCurrencyId, T model);
        Task<List<T>> ConvertToCurrencyValue<T>(long fromCurrencyId,string toCurrency, List<T> models);
        Task<T> ConvertToCurrencyValue<T>(long fromCurrencyId,string toCurrency, T model);
        Task<decimal> GetExchangeRate(long fromCurrencyId,long toCurrencyId);
        Task<Currency> GetCurrencyByCode(string code);
    }
}
