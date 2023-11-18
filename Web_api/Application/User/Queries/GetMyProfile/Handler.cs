using Application.Features.Common.Interfaces;
using Application.Interface;
using Application.Interface.Context;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.User.Queries.GetMyProfile;

public class Handler : IRequestHandler<GetMyProfileQuery, GetMyProfileDto>
{
	private readonly IApplicationDbContext _context;
	private readonly ICurrentUserService _currentUserService;
	private readonly IImageStorageService _imageStorageService;
	private readonly IMapper _mapper;
	private readonly ICurrencyService _currencyService;
	private readonly IRequestContext _requestContext;


    public Handler(ICurrentUserService currentUserService, IMapper mapper,
        IImageStorageService imageStorageService, IApplicationDbContext context, 
		ICurrencyService currencyService, 
		IRequestContext requestContext)
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
        _imageStorageService = imageStorageService;
        _context = context;
        _currencyService = currencyService;
        _requestContext = requestContext;
    }

    public async Task<GetMyProfileDto> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
	{
		// var user = _mapper.Map<GetDriverProfileDto>(await _userManager.FindByIdAsync(_currentUserService.UserId));
		var user = await _context.ApplicationUsers
				.AsNoTracking()
				.Where(c => c.Id == _currentUserService.UserId)
				.ProjectTo<GetMyProfileDto>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync(cancellationToken)
			;

		if (user.ImageId != null)
			user.ImageUrl = await _imageStorageService.GetImageURL(user.ImageId.Value);

		user.TotalSpending = await _context.ClientOrders
			.Where(c =>
				c.Active == 1
				&& c.ClientId == _currentUserService.UserId
				&& c.ClientOrderStatus != ClientOrderStatuses.New
			)
			.SumAsync(s => s.Cost, cancellationToken);
		user.OrdersCount = await _context.ClientOrders
			.Where(c =>
				c.Active == 1
				&& c.ClientId == _currentUserService.UserId
				&& c.ClientOrderStatus != ClientOrderStatuses.New
			)
			.CountAsync(cancellationToken);
		user = await _currencyService.ConvertToCurrencyValue<GetMyProfileDto>(1, _requestContext.Currency, user);
		return user;
	}
}
