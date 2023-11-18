using Application.Extentions;
using Application.Features.Common.Interfaces;
using Application.Interface.Context;
using AutoMapper;
using Domain.Model;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace Application.Features.ClientOrders.Queries.GetClientOrderReportByPromotionalId
{
    public class GetClientOrderReportByPromotionalIdQuery : SieveModel, IRequest<PagedList<GetClientOrderReportByPromotionalIdResponse>>
    {
        /// <summary>
        /// Should be pass Start date
        /// </summary>
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class Validator : AbstractValidator<GetClientOrderReportByPromotionalIdQuery>
    {
        public Validator()
        {
            RuleFor(e => e.StartDate)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.EndDate)
                .NotNull()
                .NotEmpty();
        }
    }

    public class GetClientOrderReportByPromotionalIdQueryHandler : IRequestHandler<GetClientOrderReportByPromotionalIdQuery, PagedList<GetClientOrderReportByPromotionalIdResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICancellationTokenContext _cancellationTokenContext;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly SieveOptions _sieveOptions;

        public GetClientOrderReportByPromotionalIdQueryHandler(
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

        public async Task<PagedList<GetClientOrderReportByPromotionalIdResponse>> Handle(GetClientOrderReportByPromotionalIdQuery query, CancellationToken cancellationToken)
        {
            var clientOrderQuery = _context.ClientOrders
                .Include(x => x.Client)
                .Where(x => x.Active == 1 && x.Created.Date >= query.StartDate && x.Created.Date <= query.EndDate);

            var result = await _sieveProcessor.GetPagedAsync(clientOrderQuery, _sieveOptions, query, _cancellationTokenContext.CurrentCancellationToken);

            var mappedResult = _mapper.Map<PagedList<GetClientOrderReportByPromotionalIdResponse>>(result);

            return mappedResult;
        }
    }
}
