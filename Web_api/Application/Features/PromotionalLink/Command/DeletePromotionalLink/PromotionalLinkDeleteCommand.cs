using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Application.Features.PromotionalLink.Command.UpdatePromotionalLink;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Features.PromotionalLink.Command.DeletePromotionalLink
{
    public class PromotionalLinkDeleteCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
    public class Validator : AbstractValidator<PromotionalLinkUpdateCommand>
    {
        public Validator()
        {
            RuleFor(e => e.Id)
                .NotNull()
                .NotEmpty();
        }
    }
    public class PromotionalLinkDeleteCommandHandler : IRequestHandler<PromotionalLinkDeleteCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public PromotionalLinkDeleteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(PromotionalLinkDeleteCommand request, CancellationToken cancellationToken)
        {
            var promotionalLink = await _context.PromotionalLinks
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.Status && x.Active == 1, cancellationToken) 
                ?? throw new AppNotFoundException("Promotional link not found");

            promotionalLink.Active = 0;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
