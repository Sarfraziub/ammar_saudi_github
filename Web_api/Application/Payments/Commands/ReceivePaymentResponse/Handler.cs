using Application.Features.ClientOrders.Commands.CompleteClientOrderPayment;
using Application.Features.Common.Interfaces;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Application.Payments.Commands.ReceivePaymentResponse;

public class Handler : IRequestHandler<ReceivePaymentResponseCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public Handler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
    {
        _context = context;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(ReceivePaymentResponseCommand request, CancellationToken cancellationToken)
    {
        var paymentTry = await _context.PaymentTries.Where(c => c.TransactionReference == request.TransactionReference)
            .SingleAsync(cancellationToken);

        var entity = _mapper.Map<PaymentResponse>(request);
        entity.PaymentTryId = paymentTry.Id;
        _context.PaymentResponses.Add(entity);

        var clientOrderPayments = await _context.ClientOrderPayments.SingleOrDefaultAsync(x => x.TransactionReference == request.TransactionReference);
        clientOrderPayments.PaymentResponse = JsonConvert.SerializeObject(request);
        clientOrderPayments.CartId = request.CartId;

        await _context.SaveChangesAsync(cancellationToken);

        if (entity.ResponseStatus == "A")
        {
            await _mediator.Send(new CompleteClientOrderPaymentCommand { PaymentTryId = paymentTry.Id }, cancellationToken);

        }
        return Unit.Value;
    }
}


