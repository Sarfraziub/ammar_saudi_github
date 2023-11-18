using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ManageClaims.Queries.GetClaimById;

public class Handler : IRequestHandler<GetClaimByIdQuery, GetClaimByIdDto>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;
	private readonly IImageStorageService _imageStorageService;

	public Handler(IApplicationDbContext context, IMapper mapper, IImageStorageService imageStorageService)
	{
		_context = context;
		_mapper = mapper;
		_imageStorageService = imageStorageService;
	}

	public async Task<GetClaimByIdDto> Handle(GetClaimByIdQuery request, CancellationToken cancellationToken)
	{
		var driverClaim =  await _context.DriverClaims
				.AsNoTracking()
				.Where(c => c.Id == request.Id && c.Active == 1)
				.ProjectTo<GetClaimByIdDto>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync(cancellationToken)
			;
		if (driverClaim.ReceiptId != null)
			driverClaim.ReceiptUrl = await _imageStorageService.GetImageURL(driverClaim.ReceiptId.Value);
		return driverClaim;

	}
}


