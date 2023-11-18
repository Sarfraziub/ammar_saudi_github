using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.ManageClaims.Commands.CloseClaim;

public class Handler : IRequestHandler<CloseClaimCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly IErrorMessagesService _errorMessagesService;

	public Handler(IApplicationDbContext context,IErrorMessagesService errorMessagesService)
	{
		_context = context;
		_errorMessagesService = errorMessagesService;
	}

	public async Task<Unit> Handle(CloseClaimCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.DriverClaims.FindAsync(request.Id);
		if(entity != null && entity.DriverClaimStatus == DriverClaimStatuses.Completed)
				throw new AppBadRequestException(_errorMessagesService.GetCommonErrorMessageById(4));
		if (entity == null) return Unit.Value;
		entity.DriverClaimStatus = DriverClaimStatuses.Completed;
		entity.ReceiptId = request.ReceiptId;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


