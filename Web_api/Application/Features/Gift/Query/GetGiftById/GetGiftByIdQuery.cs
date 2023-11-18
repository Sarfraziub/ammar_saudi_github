using Application.Features.Common.Interfaces;
using Application.Interface.Context;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace Application.Features.Gift.Query.GetGiftByClientOrderId
{
    public class GetGiftByIdQuery : IRequest<GetGiftByIdResponse>
    {
        public int Id { get; set; }
    }

    public class GetGiftByClientOrderIdQueryHandler : IRequestHandler<GetGiftByIdQuery, GetGiftByIdResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICancellationTokenContext _cancellationTokenContext;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly SieveOptions _sieveOptions;

        public GetGiftByClientOrderIdQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            ISieveProcessor sieveProcessor,
            IOptions<SieveOptions> sieveOptions,
            ICancellationTokenContext cancellationTokenContext)
        {
            _context = context;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
            _sieveOptions = sieveOptions.Value;
            _cancellationTokenContext = cancellationTokenContext;
        }

        public async Task<GetGiftByIdResponse> Handle(GetGiftByIdQuery request, CancellationToken cancellationToken)
        {
            var gift = await _context.Gifts.FirstOrDefaultAsync(x => x.ClientOrderId == request.Id && x.Active == 1,
                cancellationToken);

            var mappedResult = _mapper.Map<GetGiftByIdResponse>(gift);
            return mappedResult;
        }
    }
}
