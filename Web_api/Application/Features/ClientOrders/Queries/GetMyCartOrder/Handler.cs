using Application.Features.Common.Interfaces;
using Application.Interface;
using Application.Interface.Context;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Queries.GetMyCartOrder;

public class Handler : IRequestHandler<GetMyCartOrderQuery, GetMyCartOrderDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ICurrencyService _currencyService;
    private readonly IRequestContext _requestContext;

    public Handler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService, ICurrencyService currencyService, IRequestContext requestContext)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _currencyService = currencyService;
        _requestContext = requestContext;
    }

    public async Task<GetMyCartOrderDto> Handle(GetMyCartOrderQuery request, CancellationToken cancellationToken)
    {
        var taxOrderSetting = await _context.OrderSettings
            .Where(c => c.Active == 1 && c.OrderSettingType == OrderSettingTypes.Tax)
            .OrderByDescending(o => o.Created)
            .Take(1)
            .SingleAsync(cancellationToken);

        var dto = new GetMyCartOrderDto();
        if (_currentUserService.UserId != null)
            dto = await _context.ClientOrders
                .AsNoTracking()
                .Where(c =>
                    c.Active == 1
                    && c.ClientId == _currentUserService.UserId
                    && c.ClientOrderStatus == ClientOrderStatuses.New
                )
                .ProjectTo<GetMyCartOrderDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);
        else
        {
            dto = await _context.ClientOrders
                .AsNoTracking()
                .Where(c =>
                    c.Active == 1
                    && c.DeviceId == request.DeviceId
                    && c.ClientOrderStatus == ClientOrderStatuses.New
                )
                .ProjectTo<GetMyCartOrderDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

        }

        if (dto != null)
        {
            if (dto.TotalAfterDiscount != dto.Total)
            {
                dto.Tax = dto.TotalAfterDiscount * taxOrderSetting.Value;
            }
            else
            {
                dto.Tax = dto.Total * taxOrderSetting.Value;
            }

            dto.FinalTotal = dto.TotalAfterDiscount + dto.DeliveryFee + dto.Tax;
            dto.FinalTotalDefaultCurrency = dto.FinalTotal;
            dto = await _currencyService.ConvertToCurrencyValue(1, _requestContext.Currency, dto);
            return dto;
        }

        return null;
    }
}
