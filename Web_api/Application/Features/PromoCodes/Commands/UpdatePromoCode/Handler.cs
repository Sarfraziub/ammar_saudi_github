using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PromoCodes.Commands.UpdatePromoCode;

public class Handler : IRequestHandler<UpdatePromoCodeCommand, Unit>
{
	private readonly IApplicationDbContext _context;
    private readonly IErrorMessagesService _errorMessagesService;

    public Handler(IApplicationDbContext context, IErrorMessagesService errorMessagesService)
	{
		_context = context;
        _errorMessagesService = errorMessagesService;
    }

    public async Task<Unit> Handle(UpdatePromoCodeCommand request, CancellationToken cancellationToken)
	{

        if (await _context.PromoCodes.AnyAsync(a => a.Code.ToLower() == request.Code.ToLower() && a.Id != request.Id && a.Active == 1))
        {
            throw new AppBadRequestException(_errorMessagesService.GetLookupErrorMessageById(1));
        }

        var entity = await _context.PromoCodes.FindAsync(request.Id);

		var clientOrders = await _context.ClientOrders
			.Where(w => w.Active == 1 && w.PromoCodeId == request.Id)
			.ToListAsync(cancellationToken);
		if (clientOrders.Count > 0)
		{
			if (entity.Percentage != request.Percentage)
				throw new ApplicationException("Cant update percentage");
			if (entity.Code != request.Code)
				throw new ApplicationException("Cant update code");
			entity.Expiry = request.Expiry;
		}
		else
		{
			entity.Code = request.Code;
			entity.Expiry = request.Expiry;
			entity.Percentage = request.Percentage;
        }
        entity.ApplicableType = (int)request.ApplicableType;

        await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


