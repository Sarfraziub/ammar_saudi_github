using Application.Features.Common.Interfaces;
using AutoMapper;
using Domain;
using Domain.DbModel;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ActionTracking.Command.AddActionTrackingHistory
{
    public class AddActionTrackingHistoryCommand : IRequest<Unit>
    {
        public string DeviceId { get; set; }
        public string Ip { get; set; }
        public string ActionTrackingKey { get; set; }
        public string? Platform { get; set; }
        public string? PromotionalLinkKey { get; set; }
    }

    public class Validator : AbstractValidator<AddActionTrackingHistoryCommand>
    {
        public Validator()
        {
            RuleFor(e => e.DeviceId)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.Ip)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.ActionTrackingKey)
                .NotNull()
                .NotEmpty();
        }
    }
    public class AddActionTrackingHistoryCommandHandler : IRequestHandler<AddActionTrackingHistoryCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public AddActionTrackingHistoryCommandHandler(IApplicationDbContext context, 
            IMapper mapper, 
            IMediator mediator, 
            ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(AddActionTrackingHistoryCommand command, CancellationToken cancellationToken)
        {
            var key = await _context.ActionTrackings.FirstOrDefaultAsync(x => x.Name == command.ActionTrackingKey,cancellationToken);
            var promotionalLink = new Domain.DbModel.PromotionalLink();
            if (key == null)
            {
                throw new Exception("Key not found");
            }

            if (command.PromotionalLinkKey != null)
            {
                promotionalLink = await _context.PromotionalLinks
                    .FirstOrDefaultAsync(x => x.Status && x.Active == 1 && x.UniqueId == command.PromotionalLinkKey, cancellationToken);
            }
            _context.ActionTrackingHistories.Add(new ActionTrackingHistory()
            {
                DeviceId = command.DeviceId,
                Ip = command.Ip,
                UserId = (long)_currentUserService.UserId,
                ActionTrackingId = key.Id,
                PromotionalLinkId = promotionalLink?.Id,
                Platform = command.Platform?.ToUpper()
            });
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
