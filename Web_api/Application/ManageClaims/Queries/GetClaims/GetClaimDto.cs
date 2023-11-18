using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;
using EnumStringValues;

namespace Application.ManageClaims.Queries.GetClaims;

public class GetClaimDto: IMapFrom<DriverClaim>
{
	public long Id { get; set; }
	public string DriverClaimNumber { get; set; }
	public DateTime Created { get; set; }
	public DriverClaimStatuses DriverClaimStatus { get; set; }
	public string DriverClaimStatusName { get; set; }
	public string DriverNumber { get; set; }
	public string DriverName { get; set; }
	public long? ReceiptId { get; set; }
	public string ReceiptUrl { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<DriverClaim, GetClaimDto>()
			.ForMember(d => d.DriverClaimStatusName,
				opts => opts.MapFrom(s => s.DriverClaimStatus.GetStringValue()))
			.ForMember(d => d.DriverNumber,
				opts => opts.MapFrom(s => s.Driver.PhoneNumber))
			.ForMember(d => d.DriverName,
				opts => opts.MapFrom(s => s.Driver.Name))

			;
	}

}
