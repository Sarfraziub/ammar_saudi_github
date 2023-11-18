using Application.Features.Common.Interfaces;
using Application.Interface;
using Application.Interface.Context;
using Microsoft.EntityFrameworkCore;
using Domain.Attribute;
using Domain.DbModel;

namespace Application.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IRequestContext _requestContext;
        private readonly IApplicationDbContext _context;


        public CurrencyService(
            IRequestContext requestContext, 
            IApplicationDbContext context)
        {
            _requestContext = requestContext;
            _context = context;
        }

        public async Task<List<T>> ConvertToCurrencyValue<T>(long fromCurrencyId, long toCurrencyId, List<T> models)
        {
            var exchangeRate = await _context.ExchangeRates.FirstOrDefaultAsync(x => x.FromCurrencyId == fromCurrencyId && x.ToCurrencyId == toCurrencyId);

            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                var attrs = prop.GetCustomAttributes(false);
                foreach (var attr in attrs)
                {
                    if (attr is MultiCurrency)
                    {
                        foreach (var model in models)
                        {
                            var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            if (type.Name == "Decimal")
                            {
                                var value = Convert.ToDecimal(model.GetType().GetProperty(prop.Name)?.GetValue(model, null));
                                var exchangedValue = value * exchangeRate?.Rate;

                                object safeValue = (value == null) ? null : Convert.ChangeType(exchangedValue, type);

                                prop.SetValue(model, safeValue, null);
                                //prop.SetValue(model, Convert.ChangeType(exchangedValue.ToString(), prop.PropertyType), null);
                            }
                        }
                    }
                }
            }
            return models;
        }

        public async Task<T> ConvertToCurrencyValue<T>(long fromCurrencyId, long toCurrencyId, T model)
        {
            return (await ConvertToCurrencyValue(fromCurrencyId, toCurrencyId, new List<T> { model })).First();
        }
        public async Task<List<T>> ConvertToCurrencyValue<T>(long fromCurrencyId, string toCurrency, List<T> models)
        {
            var currencyToConvert = (await _context.Currencies.FirstOrDefaultAsync(x => x.Code.ToLower() == toCurrency.ToLower()))?.Id;

            if (currencyToConvert == null)
                throw new Exception("Invalid currency selected");

            return await ConvertToCurrencyValue(fromCurrencyId, (long)currencyToConvert, models);
        }

        public async Task<T> ConvertToCurrencyValue<T>(long fromCurrencyId, string toCurrency, T model)
        {
            var currencyToConvert = (await _context.Currencies.FirstOrDefaultAsync(x => x.Code.ToLower() == toCurrency.ToLower()))?.Id;

            if (currencyToConvert == null)
                throw new Exception("Invalid currency selected");

            return (await ConvertToCurrencyValue(fromCurrencyId, (long)currencyToConvert, new List<T> { model })).First();
        }

        public async Task<decimal> GetExchangeRate(long fromCurrencyId, long toCurrencyId)
        {
            return (await _context.ExchangeRates.FirstOrDefaultAsync(x => x.FromCurrencyId == fromCurrencyId && x.ToCurrencyId == toCurrencyId)).Rate;
        }

        public async Task<Currency> GetCurrencyByCode(string code)
        {
            var currency = await _context.Currencies.FirstOrDefaultAsync(x=> x.Code.ToLower() == code.ToLower() && x.Active == 1);
            if (currency == null) throw new Exception("Currency not valid");

            return currency;
        }
    }
}
