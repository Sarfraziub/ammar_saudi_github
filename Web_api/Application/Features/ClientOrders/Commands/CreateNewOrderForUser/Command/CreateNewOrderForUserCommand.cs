using Application.Features.ClientOrders.Commands.AddClientOrder;
using Application.Features.ClientOrders.Queries.GetMyCartOrder;
using Application.Features.Common.Interfaces;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Commands.CreateNewOrderForUser.Command
{
    public class CreateNewOrderForUserCommand : IRequest<long>
    {
        public int UserId { get; set; }
        public long SalesItemId { get; set; }
    }

    public class CreateNewOrderForUserCommandHandler : IRequestHandler<CreateNewOrderForUserCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IDateTime _dateTime;

        public CreateNewOrderForUserCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator, IDateTime dateTime)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _dateTime = dateTime;
        }

        public async Task<long> Handle(CreateNewOrderForUserCommand command, CancellationToken cancellationToken)
        {
            long clientOrderId = 0;
            DriverFee driverFee = null;
            var clientOrder = await _context
                .ClientOrders
                .Where(co =>
                        co.ClientId == command.UserId
                        && co.ClientOrderStatus == ClientOrderStatuses.New
                        && co.Active == 1
                    // && co.LocationId == request.LocationId
                ).SingleOrDefaultAsync(cancellationToken);

            if (clientOrder == null)
            {
                var id = await _mediator.Send(new AddClientOrderCommand
                {
                    ServiceType = ServiceTypes.MostNeeded,
                }, cancellationToken);
                clientOrderId = id;
            }
            else
            {
                clientOrderId = clientOrder.Id;

            }

            clientOrder = await _context
                .ClientOrders
                .Include(i => i.ClientOrderDetails)
                .SingleOrDefaultAsync(w => w.Id == clientOrderId, cancellationToken);

            foreach (var orderDetail in clientOrder.ClientOrderDetails)
                orderDetail.Active = 0;

            await _context.SaveChangesAsync(cancellationToken);



            var saleItem = await _context.SaleItems.FindAsync(command.SalesItemId);

            var clientOrderDetail = new ClientOrderDetail
            {
                ClientOrderId = clientOrderId,
                SaleItemId = saleItem.Id,
                Price = saleItem.Price,
                Quantity = 1
            };
            _context.ClientOrderDetails.Add(clientOrderDetail);
            await _context.SaveChangesAsync(cancellationToken);



            driverFee = await _context.DriverFees
                .Where(x => x.IsOffer && x.Active == 1 && x.StartDate <= _dateTime.UtcNow && x.EndDate >= _dateTime.UtcNow)
                .OrderByDescending(x => x.Created)
                .FirstOrDefaultAsync(cancellationToken);
            if (driverFee == null)
            {
                driverFee = await _context.DriverFees
                    .Where(w => w.Active == 1 && !w.IsOffer)
                    .OrderByDescending(d => d.Created)
                    .SingleOrDefaultAsync(cancellationToken);
            }
            if (driverFee != null)
            {
                clientOrder.DriverFeeId = driverFee.Id;
                if (driverFee.FeeType == FeeTypes.StaticFee)
                {
                    clientOrder.DeliveryFee = driverFee.Value;
                }
                else
                {
                    var getMyCartOrderDto = await _mediator.Send(new GetMyCartOrderQuery(), cancellationToken);
                    clientOrder.DeliveryFee = getMyCartOrderDto.Total * driverFee.Value;
                }
            }
            else
            {
                clientOrder.DeliveryFee = 0;
                clientOrder.DriverFeeId = null;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return clientOrder.Id;
        }
    }
}
