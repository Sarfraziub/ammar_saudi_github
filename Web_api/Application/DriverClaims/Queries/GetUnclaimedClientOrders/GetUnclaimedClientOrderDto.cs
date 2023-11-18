using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;
using EnumStringValues;

namespace Application.DriverClaims.Queries.GetUnclaimedClientOrders;

public class GetUnclaimedClientOrderDto : IMapFrom<ClientOrder>
{
	public long Id { get; set; }
	public DateTime Created { get; set; }
	public string Number { get; set; }
	public decimal Fee { get; set; }
	public FeeTypes FeeType { get; set; }
	public decimal Total { get; set; }
	public DriverClaimStatuses? DriverClaimStatus { get; set; }
	public string DriverClaimStatusName { get; set; }


	public void Mapping(Profile profile)
	{
		profile.CreateMap<ClientOrder, GetUnclaimedClientOrderDto>()
			.ForMember(d => d.Total,
				opts =>
					opts.MapFrom(s =>
						s.ClientOrderDetails.Where(f => f.Active == 1).Sum(dd => dd.Price * dd.Quantity)))
			.ForMember(d => d.Fee,
				opts => opts.MapFrom(s => s.DriverFee.Value))
			.ForMember(d => d.FeeType,
				opts => opts.MapFrom(s => s.DriverFee.FeeType))
			.ForMember(d => d.DriverClaimStatus,
				opts => opts.MapFrom(s => s.DriverClaim != null ? s.DriverClaim.DriverClaimStatus : null))
			.ForMember(d => d.DriverClaimStatusName,
				opts => opts.MapFrom(s =>
					s.DriverClaim != null ? s.DriverClaim.DriverClaimStatus.GetStringValue() : null))
			;
	}
}
