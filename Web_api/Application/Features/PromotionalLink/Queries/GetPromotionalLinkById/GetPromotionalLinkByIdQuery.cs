using Application.Features.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PromotionalLink.Queries.GetPromotionalLinkById
{
    public class GetPromotionalLinkByIdQuery : IRequest<GetPromotionalLinkByIdResponse>
    {
        public int Id { get; set; }
    }


    public class GetPromotionalLinkByIdQueryHandler : IRequestHandler<GetPromotionalLinkByIdQuery, GetPromotionalLinkByIdResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPromotionalLinkByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetPromotionalLinkByIdResponse> Handle(GetPromotionalLinkByIdQuery query, CancellationToken cancellationToken)
        {
            var promotionalLink = await _context.PromotionalLinks
                .FirstOrDefaultAsync(x => x.Id == query.Id && x.Status && x.Active == 1, cancellationToken);

            //if(promotionalLink == null)
            //{
            //    throw new AppNotFoundException("Promotional link not found");
            //}
            var mappedResult = _mapper.Map<GetPromotionalLinkByIdResponse>(promotionalLink);
            
            return mappedResult;
        }
    }
}
