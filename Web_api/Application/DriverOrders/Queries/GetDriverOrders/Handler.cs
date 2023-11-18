using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverOrders.Queries.GetDriverOrders;

public class Handler : IRequestHandler<GetDriverOrdersQuery, GetDriverOrdersVm>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public Handler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<GetDriverOrdersVm> Handle(GetDriverOrdersQuery request, CancellationToken cancellationToken)
    {
        var vm = new GetDriverOrdersVm
        {
            ClientOrders = await _context.ClientOrders
                .AsNoTracking()
                .Where(c =>
                    c.Active == 1
                    // && (c.ClientOrderStatus == ClientOrderStatuses.Delivered ||
                    //     c.ClientOrderStatus == ClientOrderStatuses.WithDriver)
                    && c.DriverId == _currentUserService.UserId
                    && (string.IsNullOrEmpty(request.Number) ||
                        c.Number.Contains(request.Number))
                    && (request.StartDate == null ||
                        c.Created >= request.StartDate)
                    && (request.EndDate == null ||
                        c.Created >= request.EndDate)
                    && (request.ClientOrderStatus == null ||
                        c.ClientOrderStatus == request.ClientOrderStatus)
                )
                .ProjectTo<GetDriverOrderDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
        foreach (var clientOrder in vm.ClientOrders)
        {
	        if (clientOrder.FeeType == FeeTypes.Percentage)
		        clientOrder.Fee = clientOrder.Total * clientOrder.Fee;
	        else
		        clientOrder.Fee = clientOrder.Fee;
        }
        return vm;
    }
}
