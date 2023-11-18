using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Application.RatesAndFeedbacks.Commands.SetRateAndFeedback;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RatesAndFeedbacks.Commands.UpdateFlagSelected
{
    public class Handler : IRequestHandler<UpdateFlagCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateFlagCommand request, CancellationToken cancellationToken)
        {
            var clientOrder = await _context.ClientOrders
                .SingleOrDefaultAsync(w => w.Id == request.ClientOrderId, cancellationToken);
            if (clientOrder == null) throw new AppNotFoundException("not found");
            clientOrder.FlgSelected = request.FlgSelected;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
