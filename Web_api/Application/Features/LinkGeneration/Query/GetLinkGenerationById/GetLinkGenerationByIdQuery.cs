using Application.Features.Common.Interfaces;
using Application.Features.LinkGeneration.Query.GetLinkGenerationById;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LinkGeneration.Query.GetById
{
    public class GetLinkGenerationByUniqueIdQuery : IRequest<GetLinkGenerationByUniqueIdResponse>
    {
        public string UniqueId { get; set; }
    }

    public class GetLinkGenerationByIdUniqueQueryHandler : IRequestHandler<GetLinkGenerationByUniqueIdQuery, GetLinkGenerationByUniqueIdResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetLinkGenerationByIdUniqueQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetLinkGenerationByUniqueIdResponse> Handle(GetLinkGenerationByUniqueIdQuery query, CancellationToken cancellationToken)
        {
            var linkGeneration = await _context.LinkGenerations
                .Include(x=> x.LinkGenerationValue)
                .FirstOrDefaultAsync(x => x.UniqueId == query.UniqueId && x.IsValid && x.Active == 1, cancellationToken);

            var mappedResult = _mapper.Map<GetLinkGenerationByUniqueIdResponse>(linkGeneration);
            return mappedResult;
        }
    }
}
