using Application.Features.Common.Interfaces;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.SaleItems.Commands.AddSaleItem;

public class Handler : IRequestHandler<AddSaleItemCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;
	private readonly IMediator _mediator;

	public Handler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
	{
		_context = context;
		_mapper = mapper;
		_mediator = mediator;
	}

	public async Task<Unit> Handle(AddSaleItemCommand request, CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<SaleItem>(request);
		_context.SaleItems.Add(entity);
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


