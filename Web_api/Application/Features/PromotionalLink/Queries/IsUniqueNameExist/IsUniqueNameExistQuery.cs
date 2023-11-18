using Application.Features.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PromotionalLink.Queries.IsUniqueNameExist
{
    public class IsUniqueNameExistQuery : IRequest<bool>
    {
        [BindNever]
        public string UniqueName { get; set; }
        public long? PromotionalLinkId { get; set; }
    }


    public class IsUniqueNameExistQueryHandler : IRequestHandler<IsUniqueNameExistQuery, bool>
    {
        private readonly IApplicationDbContext _context;

        public IsUniqueNameExistQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(IsUniqueNameExistQuery request, CancellationToken cancellationToken)
        {
            var promotionalLinkQuery =
                _context.PromotionalLinks.Where(x => x.UniqueName.ToLower() == request.UniqueName.ToLower());
            if (request.PromotionalLinkId is not null)
            {
                promotionalLinkQuery = promotionalLinkQuery.Where(x => x.Id != request.PromotionalLinkId);
            }
            var promotionalLink = await promotionalLinkQuery
                .FirstOrDefaultAsync(cancellationToken);

            return promotionalLink != null;
        }
    }
}
