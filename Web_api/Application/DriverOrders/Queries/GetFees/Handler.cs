using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverOrders.Queries.GetFees;

public class Handler : IRequestHandler<GetFeesQuery, GetFeesDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;
    private readonly IMapper _mapper;

    public Handler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService,
        IDateTime dateTime)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _dateTime = dateTime;
    }

    public async Task<GetFeesDto> Handle(GetFeesQuery request, CancellationToken cancellationToken)
    {
        var fees = new GetFeesDto();
        var todayVm = new GetOrderFeesVm
        {
            ClientOrders = await _context.ClientOrders
                .AsNoTracking()
                .Where(c =>
                    c.Active == 1
                    && c.DriverId == _currentUserService.UserId
                    && c.DeliveryTime >= _dateTime.StartOfDay(DateTime.Now)
                    && c.DeliveryTime <= _dateTime.EndOfDay(DateTime.Now)
                    && c.ClientOrderStatus == ClientOrderStatuses.Delivered
                )
                .ProjectTo<GetOrderFeesDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };

        foreach (var clientOrder in todayVm.ClientOrders)
        {
            if (clientOrder.FeeType == FeeTypes.Percentage)
                fees.TodayTotalFees += clientOrder.Total * clientOrder.Fee;
            else
                fees.TodayTotalFees += clientOrder.Fee;
        }
        var weeklyVm = new GetOrderFeesVm
        {
            ClientOrders = await _context.ClientOrders
                .AsNoTracking()
                .Where(c =>
                    c.Active == 1
                    && c.DriverId == _currentUserService.UserId
                    && c.Created >= _dateTime.StartOfDay(DateTime.Now.AddDays(-7))
                    && c.Created <= _dateTime.EndOfDay(DateTime.Now)
                    && c.ClientOrderStatus == ClientOrderStatuses.Delivered
                )
                .ProjectTo<GetOrderFeesDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };

        foreach (var clientOrder in weeklyVm.ClientOrders)
        {
            if (clientOrder.FeeType == FeeTypes.Percentage)
                fees.WeeklyTotalFees += clientOrder.Total * clientOrder.Fee;
            else
                fees.WeeklyTotalFees += clientOrder.Fee;
        }

        return fees;
    }
}
