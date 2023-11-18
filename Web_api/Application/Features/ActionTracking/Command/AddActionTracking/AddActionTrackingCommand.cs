using System.Security.Cryptography.X509Certificates;
using Application.Features.Common.Interfaces;
using Application.Features.PromotionalLink.Command.AddPromotionalLink;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ActionTracking.Command.AddTracking
{
    public class AddActionTrackingCommand : IRequest<Unit>
    {
        public string Name { get; set; }
    }

    public class Validator : AbstractValidator<AddPromotionalLinkCommand>
    {
        public Validator()
        {
            RuleFor(e => e.Name)
                .NotNull()
                .NotEmpty();
        }
    }
    public class AddTrackingCommandHandler : IRequestHandler<AddActionTrackingCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public AddTrackingCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddActionTrackingCommand command, CancellationToken cancellationToken)
        {
            var key = await _context.ActionTrackings.FirstOrDefaultAsync(x => x.Name == command.Name, cancellationToken);
            if (key != null)
            {
                throw new Exception("Key already exist");
            }
            _context.ActionTrackings.Add(new Domain.DbModel.ActionTracking()
            {
                Name = command.Name
            });
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
