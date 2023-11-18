using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;
using EnumStringValues;

namespace Application.DriverClaims.Queries.GetDriverClaims;

public class GetDriverClaimDto : IMapFrom<DriverClaim>
{
	public long Id { get; set; }
	public string DriverClaimNumber { get; set; }
	public DateTime Created { get; set; }
	public DriverClaimStatuses DriverClaimStatus { get; set; }
	public string DriverClaimStatusName { get; set; }
	public string ReceiptUrl { get; set; }
	public long? ReceiptId { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<DriverClaim, GetDriverClaimDto>()
			.ForMember(d => d.DriverClaimStatusName,
				opts => opts.MapFrom(s => s.DriverClaimStatus.GetStringValue()))

			;
	}

}
