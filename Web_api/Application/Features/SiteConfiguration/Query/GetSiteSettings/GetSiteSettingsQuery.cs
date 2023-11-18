using Application.Features.Common.Interfaces;
using Application.Features.LinkGeneration.Query.GetAllLinkGeneration;
using Application.Interface.Context;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SiteConfiguration.Query.GetSiteSettings
{
    public class GetSiteSettingsQuery : IRequest<GetSiteSettingsResponse>
    {
    }

    public class GetSiteSettingsQueryHandler : IRequestHandler<GetSiteSettingsQuery, GetSiteSettingsResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICancellationTokenContext _cancellationTokenContext;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly SieveOptions _sieveOptions;

        public GetSiteSettingsQueryHandler(
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

        public async Task<GetSiteSettingsResponse> Handle(GetSiteSettingsQuery query, CancellationToken cancellationToken)
        {
            var siteSettings = await _context.SiteConfigurations
                .SingleOrDefaultAsync(x => x.Active == 1, cancellationToken);
            var mappedResult = _mapper.Map<GetSiteSettingsResponse>(siteSettings);
            return mappedResult;
        }
    }
}
