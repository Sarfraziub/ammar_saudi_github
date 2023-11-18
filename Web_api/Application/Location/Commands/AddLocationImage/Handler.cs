using Application.Features.Common.Interfaces;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.Location.Commands.AddLocationImage;

public class Handler : IRequestHandler<AddLocationImageCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(AddLocationImageCommand request, CancellationToken cancellationToken)
	{
		_context.LocationImages.Add(new LocationImage()
		{
			FileId = request.ImageId,
			LocationId = request.LocationId
		});

		await _context.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
