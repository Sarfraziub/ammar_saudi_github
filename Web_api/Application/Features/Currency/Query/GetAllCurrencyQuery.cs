using Application.Features.ClientOrders.Queries.GetMyCartOrder;
using Application.Features.Common.Interfaces;
using Application.Interface.Context;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace Application.Features.Currency.Query
{
    public class GetAllCurrencyQuery : IRequest<List<GetAllCurrencyResponse>>
    {
    }

    public class GetAllCurrencyQueryHandler : IRequestHandler<GetAllCurrencyQuery, List<GetAllCurrencyResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCurrencyQueryHandler(
            IApplicationDbContext context,
            IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetAllCurrencyResponse>> Handle(GetAllCurrencyQuery query, CancellationToken cancellationToken)
        {
            var currencies = await _context.Currencies
                .Where(x =>x.Active == 1)
                .ProjectTo<GetAllCurrencyResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return currencies;
        }
    }
}
