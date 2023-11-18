using Application.Features.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Gift.Command.DeleteGift
{
    public class DeleteGiftCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class DeleteGiftCommandHandler : IRequestHandler<DeleteGiftCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteGiftCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteGiftCommand command, CancellationToken cancellationToken)
        {
            var gift = await _context.Gifts.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
            if (gift != null)
            {
                gift.Active = 0;
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
