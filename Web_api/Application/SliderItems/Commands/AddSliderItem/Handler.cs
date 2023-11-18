using Application.Features.Common.Interfaces;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.SliderItems.Commands.AddSliderItem;

public class Handler : IRequestHandler<AddSliderItemCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(AddSliderItemCommand request, CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<SliderItem>(request);
		_context.SliderItems.Add(entity);
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


