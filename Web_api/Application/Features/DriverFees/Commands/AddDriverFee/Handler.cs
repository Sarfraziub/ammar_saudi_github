using Application.Features.Common.Interfaces;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.DriverFees.Commands.AddDriverFee;

public class Handler : IRequestHandler<AddDriverFeeCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Handler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(AddDriverFeeCommand request, CancellationToken cancellationToken)
    {
        DriverFee entity;

        if (request.IsOffer)
        {
            if (request.StartDate == null || request.EndDate == null)
            {
                throw new Exception("Offer start date or end date can't be null");
            }
        }
        else
        {
            entity = await _context.DriverFees.Where(w => w.Active == 1 && !w.IsOffer).OrderByDescending(d => d.Created).Take(1)
                .SingleOrDefaultAsync(cancellationToken);
            entity.Active = 0;
            request.StartDate = null;
            request.EndDate = null;
        }
        
        entity = _mapper.Map<DriverFee>(request);
        _context.DriverFees.Add(entity);
        
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}