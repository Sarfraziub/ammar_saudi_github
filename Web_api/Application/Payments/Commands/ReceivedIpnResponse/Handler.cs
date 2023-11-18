using System.Text.Json;
using Application.Features.Common.Interfaces;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Payments.Commands.ReceivedIpnResponse;

public class Handler : IRequestHandler<ReceivedIpnResponseCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Handler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(ReceivedIpnResponseCommand request, CancellationToken cancellationToken)
    {
        var paymentTry = await _context.PaymentTries.Where(c => c.TransactionReference == request.tran_ref)
            .SingleAsync(cancellationToken);

        var entity = new IpnResponse
        {
            PaymentTryId = paymentTry.Id,
            Response = JsonSerializer.Serialize(request)
        };

        _context.IpnResponses.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}


