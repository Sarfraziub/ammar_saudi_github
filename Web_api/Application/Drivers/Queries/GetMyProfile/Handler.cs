using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Drivers.Queries.GetMyProfile;

public class Handler : IRequestHandler<GetDriverProfileQuery, GetDriverProfileDto>
{
	private readonly IApplicationDbContext _context;
	private readonly ICurrentUserService _currentUserService;
	private readonly IImageStorageService _imageStorageService;
	private readonly IMapper _mapper;


	public Handler(ICurrentUserService currentUserService, IMapper mapper,
		IImageStorageService imageStorageService, IApplicationDbContext context)
	{
		_currentUserService = currentUserService;
		_mapper = mapper;
		_imageStorageService = imageStorageService;
		_context = context;
	}

	public async Task<GetDriverProfileDto> Handle(GetDriverProfileQuery request, CancellationToken cancellationToken)
	{
		// var user = _mapper.Map<GetDriverProfileDto>(await _userManager.FindByIdAsync(_currentUserService.UserId));
		var user = await _context.ApplicationUsers
				.AsNoTracking()
				.Where(c => c.Id == _currentUserService.UserId)
				.ProjectTo<GetDriverProfileDto>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync(cancellationToken)
			;

		if (user.IbanImageId != null)
			user.IbanUrl = await _imageStorageService.GetImageURL(user.IbanImageId.Value);
		if (user.ImageId != null)
			user.ImageUrl = await _imageStorageService.GetImageURL(user.ImageId.Value);
		if (user.NationalImageImageId != null)
			user.NationalIdUrl = await _imageStorageService.GetImageURL(user.NationalImageImageId.Value);
		user.TotalOrderDelivered =
			await _context
				.ClientOrders
				.Where(w =>
					w.DriverId == _currentUserService.UserId
					&& w.ClientOrderStatus == ClientOrderStatuses.Delivered
				)

				.CountAsync(cancellationToken);
		return user;
	}
}
