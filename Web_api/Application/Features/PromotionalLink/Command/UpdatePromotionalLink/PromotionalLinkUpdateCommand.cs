using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Application.Features.PromotionalLink.Queries.IsUniqueNameExist;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PromotionalLink.Command.UpdatePromotionalLink
{
    public class PromotionalLinkUpdateCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string? Email { get; set; }
        public string UniqueName { get; set; }

    }

    public class Validator : AbstractValidator<PromotionalLinkUpdateCommand>
    {
        public Validator()
        {
            RuleFor(e => e.Id)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.MobileNo)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.UniqueName)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.Email)
                .EmailAddress()
                .WithMessage("A valid email address is required");
        }
    }

    public class PromotionalLinkUpdateHandler : IRequestHandler<PromotionalLinkUpdateCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;


        public PromotionalLinkUpdateHandler(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(PromotionalLinkUpdateCommand request, CancellationToken cancellationToken)
        {
            var promotionalLink = await _context.PromotionalLinks
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.Status && x.Active == 1, cancellationToken)
                ?? throw new AppNotFoundException("Promotional link not found");

            promotionalLink.Name = request.Name;
            promotionalLink.MobileNo = request.MobileNo;
            promotionalLink.Email = request.Email;

            
            var isUniqueNameExist = await _mediator.Send(new IsUniqueNameExistQuery() { UniqueName = request.UniqueName, PromotionalLinkId = request.Id}, cancellationToken);

            if (isUniqueNameExist)
            {
                throw new AlreadyExistException("Unique name already exist");
            }
            promotionalLink.UniqueName = request.UniqueName;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
