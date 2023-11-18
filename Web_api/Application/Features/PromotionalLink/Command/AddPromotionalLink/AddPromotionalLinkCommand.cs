using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Mappings;
using Application.Features.PromotionalLink.Queries.IsUniqueNameExist;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Application.Features.PromotionalLink.Command.AddPromotionalLink
{
    public class AddPromotionalLinkCommand : IRequest<Unit>, IMapFrom<Domain.DbModel.PromotionalLink>
    {
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string? Email { get; set; }
        public string UniqueName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddPromotionalLinkCommand, Domain.DbModel.PromotionalLink>();
        }
    }

    public class Validator : AbstractValidator<AddPromotionalLinkCommand>
    {
        public Validator()
        {
            RuleFor(e => e.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.MobileNo)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.Email)
                .EmailAddress()
                .WithMessage("A valid email address is required");
        }
    }
    public class AddPromotionalLinkHandler : IRequestHandler<AddPromotionalLinkCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AddPromotionalLinkHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(AddPromotionalLinkCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.DbModel.PromotionalLink>(request);
            entity.UniqueId = Guid.NewGuid().ToString("N").Substring(0, 10);

            var isExist = await _mediator.Send(new IsUniqueNameExistQuery() { UniqueName = request.UniqueName });
            if (isExist)
            {
                throw new AlreadyExistException("Unique name already exist");
            }

            _context.PromotionalLinks.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
