using Application.Features.Common.Interfaces;
using Application.Interface.Context;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace Application.Features.LinkGeneration.Query.GetAllLinkGeneration
{
    public class GetAllLinkGenerationQuery : IRequest<List<GetAllLinkGenerationResponse>>
    {
    }

    public class GetAllLinkGenerationQueryHandler : IRequestHandler<GetAllLinkGenerationQuery, List<GetAllLinkGenerationResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICancellationTokenContext _cancellationTokenContext;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly SieveOptions _sieveOptions;

        public GetAllLinkGenerationQueryHandler(
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

        public async Task<List<GetAllLinkGenerationResponse>> Handle(GetAllLinkGenerationQuery query, CancellationToken cancellationToken)
        {
            var linkGenerations = await _context.LinkGenerations
                .Include(x => x.LinkGenerationValue)
                .Where(x => x.IsValid && x.Active == 1)
                .ToListAsync(cancellationToken);

            var mappedResult = _mapper.Map<List<GetAllLinkGenerationResponse>>(linkGenerations);
            return mappedResult;
        }
    }
}
