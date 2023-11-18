using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ManageClaims.Queries.GetClaims;

public class Handler : IRequestHandler<GetClaimsQuery, GetClaimsVm>
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

	public async Task<GetClaimsVm> Handle(GetClaimsQuery request, CancellationToken cancellationToken)
	{
		var vm = new GetClaimsVm
		{
			Claims = await _context.DriverClaims
				.AsNoTracking()
				.Where(c =>
					c.Active == 1
					&& (request.DriverClaimStatus == null || c.DriverClaimStatus == request.DriverClaimStatus)
				)
				.ProjectTo<GetClaimDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};
		foreach (var driverClaim in vm.Claims.Where(driverClaim => driverClaim.ReceiptId != null))
		{
			if (driverClaim.ReceiptId != null)
				driverClaim.ReceiptUrl = await _imageStorageService.GetImageURL(driverClaim.ReceiptId.Value);
		}
		return vm;
	}
}


