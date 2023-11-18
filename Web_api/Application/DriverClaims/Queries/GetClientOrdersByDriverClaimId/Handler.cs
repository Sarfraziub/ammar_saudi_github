using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverClaims.Queries.GetClientOrdersByDriverClaimId;

public class Handler : IRequestHandler<GetClientOrdersByDriverClaimIdQuery, GetClientOrdersByDriverClaimIdVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Handler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetClientOrdersByDriverClaimIdVm> Handle(GetClientOrdersByDriverClaimIdQuery request,
        CancellationToken cancellationToken)
    {
        var vm = new GetClientOrdersByDriverClaimIdVm
        {
            ClientOrders = await _context.ClientOrders
                .AsNoTracking()
                .Where(c =>
                    c.DriverClaimId == request.Id
                )
                .ProjectTo<GetClientOrdersByDriverClaimIdDto>(_mapper.ConfigurationProvider)
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
