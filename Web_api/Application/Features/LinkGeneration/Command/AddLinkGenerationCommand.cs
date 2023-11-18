using Application.Features.ClientOrders.Commands.CreateNewOrderForUser.Command;
using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Mappings;
using Application.Features.LinkGenerationValue.Command;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LinkGeneration.Command
{
    public class AddLinkGenerationCommand : IRequest<string>, IMapFrom<Domain.DbModel.LinkGeneration>
    {
        public bool IsValid { get; set; }
        public LinkGenerationType LinkGenerationType { get; set; }
        public bool SendWhatsapp { get; set; }
        public bool SendEmail { get; set; }
        public int UserId { get; set; }
        public long SalesItemId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddLinkGenerationCommand, Domain.DbModel.LinkGeneration>();
        }
    }

    //public class Validator : AbstractValidator<AddLinkGenerationCommand>
    //{
    //    public Validator()
    //    {
    //        RuleFor(e => e.IsValid)
    //            .NotNull()
    //            .NotEmpty();
    //        RuleFor(e => e.LinkGenerationType)
    //            .NotNull()
    //            .NotEmpty();
    //        RuleFor(e => e.SendWhatsapp)
    //            .NotNull()
    //            .NotEmpty();
    //        RuleFor(e => e.SendEmail)
    //            .NotNull()
    //            .NotEmpty();
    //    }
    //}
    public class AddLinkGenerationCommandHandler : IRequestHandler<AddLinkGenerationCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IUserManager _userManager;

        public AddLinkGenerationCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator, IJwtAuthManager jwtAuthManager, IUserManager userManager)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _jwtAuthManager = jwtAuthManager;
            _userManager = userManager;
        }

        public async Task<string> Handle(AddLinkGenerationCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.DbModel.LinkGeneration>(command);
            entity.UniqueId = Guid.NewGuid().ToString("N").Substring(0, 10);

            var uniqueIdExist = await _context.LinkGenerations.FirstOrDefaultAsync(x=> x.UniqueId == entity.UniqueId,cancellationToken);
            if (uniqueIdExist != null)
            {
                throw new AlreadyExistException("Unique id already exist");
            }

            _context.LinkGenerations.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            if (command.LinkGenerationType == LinkGenerationType.OrderPayment20)
            {
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == command.UserId,
                    cancellationToken);

                if (user == null)
                {
                    throw new AlreadyExistException("User id no exist");
                }

                var role = await _userManager.GetRoleAsync(user);
                var token = _jwtAuthManager.GenerateTokens(user.PhoneNumber, role, user.Id.ToString(),
                    user.FirstLogin.ToString());


                var clientOrderId = await _mediator.Send(new CreateNewOrderForUserCommand()
                {
                    UserId = command.UserId,
                    SalesItemId = command.SalesItemId
                }, cancellationToken);

                await _mediator.Send(new AddLinkGenerationValueCommand()
                {
                    Name = "OrderId",
                    Type = "int",
                    Value = clientOrderId.ToString(),
                    LinkGenerationId = entity.Id
                }, cancellationToken);

                await _mediator.Send(new AddLinkGenerationValueCommand()
                {
                    Name = "Token",
                    Type = "string",
                    Value = token.AccessToken,
                    LinkGenerationId = entity.Id
                }, cancellationToken);

            }
            return entity.UniqueId;
        }
    }
}
