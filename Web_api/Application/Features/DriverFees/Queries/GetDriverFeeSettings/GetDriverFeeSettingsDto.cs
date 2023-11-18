using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;

namespace Application.Features.DriverFees.Queries.GetDriverFeeSettings;

public class GetDriverFeeSettingsDto: IMapFrom<DriverFee>
{
    public long Id { get; set; }
    public FeeTypes FeeType { get; set; }
    public decimal Value { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsOffer { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<DriverFee, GetDriverFeeSettingsDto>();
    }
}