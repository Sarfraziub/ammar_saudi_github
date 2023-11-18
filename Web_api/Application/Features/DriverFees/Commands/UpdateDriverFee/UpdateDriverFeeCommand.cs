using Application.Features.Common.Interfaces;
using Application.Features.Common.Mappings;
using Application.Features.DriverFees.Commands.AddDriverFee;
using AutoMapper;
using Domain;
using Domain.DbModel;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.DriverFees.Commands.UpdateDriverFee
{
    public class UpdateDriverFeeCommand : IRequest<Unit>, IMapFrom<DriverFee>
    {
        public int Id { get; set; }
        public FeeTypes FeeType { get; set; }
        public decimal Value { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsOffer { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddDriverFeeCommand, DriverFee>();
        }
    }

    public class Validator : AbstractValidator<AddDriverFeeCommand>
    {
        public Validator()
        {
            RuleFor(e => e.Value)
                .NotNull();
            RuleFor(e => e.FeeType)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.IsOffer)
                .NotNull();
        }
    }
    public class UpdateDriverFeeCommandHandler : IRequestHandler<UpdateDriverFeeCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateDriverFeeCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDriverFeeCommand request, CancellationToken cancellationToken)
        {
            if (request.IsOffer && (request.StartDate == null || request.EndDate == null))
            {
                throw new Exception("Offer start date or end date can't be null");
            }
            
            var driverFee = await _context.DriverFees
                    .FirstOrDefaultAsync(x=> x.Id == request.Id,cancellationToken);

            driverFee.FeeType = request.FeeType;
            driverFee.Value = request.Value;
            driverFee.StartDate = request.StartDate;
            driverFee.EndDate = request.EndDate;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
