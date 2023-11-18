using Application.Features.Common.Interfaces;
using Application.Features.Common.Mappings;
using AutoMapper;
using FluentValidation;
using MediatR;


namespace Application.Features.LinkGenerationValue.Command
{
    public class AddLinkGenerationValueCommand : IRequest<Unit>, IMapFrom<Domain.DbModel.LinkGenerationValue>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public long LinkGenerationId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddLinkGenerationValueCommand, Domain.DbModel.LinkGenerationValue>();
        }
    }

    public class Validator : AbstractValidator<AddLinkGenerationValueCommand>
    {
        public Validator()
        {
            RuleFor(e => e.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.Type)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.Value)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.LinkGenerationId)
                .NotNull()
                .NotEmpty();
        }
    }
    public class LinkGenerationValueCommandHandler : IRequestHandler<AddLinkGenerationValueCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public LinkGenerationValueCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(AddLinkGenerationValueCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.DbModel.LinkGenerationValue>(command);

            _context.LinkGenerationValues.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
