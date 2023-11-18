using Application.Features.Common.Mappings;
using AutoMapper;

namespace Application.Features.Currency.Query
{
    public class GetAllCurrencyResponse : IMapFrom<Domain.DbModel.Currency>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.DbModel.Currency, GetAllCurrencyResponse>();
        }
    }
}
