using Application.Features.Common.Interfaces;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.Location.Commands.AddLocation;

public class Handler : IRequestHandler<AddLocationCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(AddLocationCommand request, CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<Domain.DbModel.Location>(request);
		_context.Locations.Add(entity);
		await _context.SaveChangesAsync(cancellationToken);

		if(request.Images != null)
		{
            foreach (var image in request.Images)
            {
                _context.LocationImages.Add(new LocationImage()
                {
                    FileId = image,
                    LocationId = entity.Id
                });
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
		return Unit.Value;
	}
}
