using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverClaims.Queries.GetDriverClaims;

public class Handler : IRequestHandler<GetDriverClaimsQuery, GetDriverClaimsVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;
	private readonly ICurrentUserService _currentUserService;
	private readonly IImageStorageService _imageStorageService;

	public Handler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService,
		IImageStorageService imageStorageService)
	{
		_context = context;
		_mapper = mapper;
		_currentUserService = currentUserService;
		_imageStorageService = imageStorageService;
	}

	public async Task<GetDriverClaimsVm> Handle(GetDriverClaimsQuery request, CancellationToken cancellationToken)
	{
		var vm = new GetDriverClaimsVm
		{
			DriverClaims = await _context.DriverClaims
				.AsNoTracking()
				.Where(c =>
					c.Active == 1
					&& c.DriverId == _currentUserService.UserId
				)
				.ProjectTo<GetDriverClaimDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};
		foreach (var driverClaim in vm.DriverClaims.Where(driverClaim => driverClaim.ReceiptId != null))
		{
			if (driverClaim.ReceiptId != null)
				driverClaim.ReceiptUrl = await _imageStorageService.GetImageURL(driverClaim.ReceiptId.Value);
		}

		return vm;
	}
}
