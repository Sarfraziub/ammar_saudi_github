using Application.Features.ClientOrders.Commands.AddClientOrderLog;
using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverClaims.Command.RequestDriverClaim;

public class Handler : IRequestHandler<RequestDriverClaimCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;
	private readonly ICurrentUserService _currentUserService;
	private readonly IMediator _mediator;
	private readonly IErrorMessagesService _errorMessagesService;

	public Handler(
		IApplicationDbContext context
		, IMapper mapper
		, ICurrentUserService currentUserService
		, IMediator mediator
		, IErrorMessagesService errorMessagesService
	)
	{
		_context = context;
		_mapper = mapper;
		_currentUserService = currentUserService;
		_mediator = mediator;
		_errorMessagesService = errorMessagesService;
	}

	public async Task<Unit> Handle(RequestDriverClaimCommand request, CancellationToken cancellationToken)
	{
		var driverClientOrders = await _context.ClientOrders
			.Where(c =>
				c.Active == 1
				&& c.DriverId == _currentUserService.UserId
				&& c.ClientOrderStatus == ClientOrderStatuses.Delivered
				&& c.DriverClaimId == null
			)
			.ToListAsync(cancellationToken);


		if (driverClientOrders.Count == 0)
			throw new AppBadRequestException(_errorMessagesService.GetCommonErrorMessageById(4));

		var count = await _context
			.DriverClaims
			.Where(c => c.Created.Year == DateTime.Now.Year)
			.CountAsync(cancellationToken);
		++count;

		var driverClaim = new DriverClaim
		{
			DriverClaimNumber = $"CM - {DateTime.Now.Year}-{count.ToString().PadLeft(5, '0')}",
			DriverClaimStatus = DriverClaimStatuses.Pending
		};
		_context.DriverClaims.Add(driverClaim);
		await _context.SaveChangesAsync(cancellationToken);


		foreach (var clientOrder in driverClientOrders)
		{
			clientOrder.DriverClaimId = driverClaim.Id;
			await _context.SaveChangesAsync(cancellationToken);

			await _mediator.Send(
				new AddClientOrderLogCommand
				{
					ClientOrderId = clientOrder.Id, Description = "",
					ClientOrderActionLogStatus = ClientOrderActionLogStatuses.DriverRequestClaim
				}, cancellationToken);
		}
		return Unit.Value;
	}
}
