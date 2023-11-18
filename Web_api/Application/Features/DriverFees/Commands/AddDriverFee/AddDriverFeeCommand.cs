using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.Features.DriverFees.Commands.AddDriverFee;

public class AddDriverFeeCommand : IRequest<Unit>, IMapFrom<DriverFee>
{
    public FeeTypes FeeType { get; set; }
    public decimal Value { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsOffer { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AddDriverFeeCommand, DriverFee>()
            ;
    }
}