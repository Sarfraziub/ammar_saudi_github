using Application.Extentions;
using Application.Features.Common.Interfaces;
using Application.Interface.Context;
using AutoMapper;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace Application.Features.PromotionalLink.Queries.GetPromotionalLinks
{
    public class GetPromotionalLinksQuery : SieveModel, IRequest<PagedList<GetPromotionalLinksResponse>>
    {
    }

    public class GetPromotionalLinksQueryHandler : IRequestHandler<GetPromotionalLinksQuery, PagedList<GetPromotionalLinksResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICancellationTokenContext _cancellationTokenContext;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly SieveOptions _sieveOptions;

        public GetPromotionalLinksQueryHandler(
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

        public async Task<PagedList<GetPromotionalLinksResponse>> Handle(GetPromotionalLinksQuery query, CancellationToken cancellationToken)
        {
            var promotionalLinksQuery = _context.PromotionalLinks
                .Where(x => x.Status && x.Active == 1);

            var result = await _sieveProcessor.GetPagedAsync(promotionalLinksQuery, _sieveOptions, query, _cancellationTokenContext.CurrentCancellationToken);

            var mappedResult = _mapper.Map<PagedList<GetPromotionalLinksResponse>>(result);

            return mappedResult;
        }
    }
}
