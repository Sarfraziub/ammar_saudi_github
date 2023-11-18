using Application.Features.Common.Interfaces;
using AutoMapper;
using Domain.DbModel;
using MediatR;

namespace Application.Features.ClientOrders.Commands.AddClientOrderLog;

public class Handler : IRequestHandler<AddClientOrderLogCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(AddClientOrderLogCommand request, CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<ClientOrderLog>(request);
		_context.ClientOrderLogs.Add(entity);
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


