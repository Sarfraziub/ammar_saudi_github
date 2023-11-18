using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;
using EnumStringValues;

namespace Application.DriverOrders.Queries.GetTodayDashboard;

public class GetTodayDashboardUnpaidDto : IMapFrom<ClientOrder>
{
	public long Id { get; set; }
	public DateTime Created { get; set; }
	public DateTime? DeliveryTime { get; set; }

	public string Number { get; set; }
	public decimal Fee { get; set; }
	public FeeTypes FeeType { get; set; }
	public decimal Total { get; set; }
	public DriverClaimStatuses? DriverClaimStatus { get; set; }
	public string DriverClaimStatusName { get; set; }

	public long LocationId { get; set; }
	public string LocationName { get; set; }
	public string LocationArabicName { get; set; }
	public string LocationUrl { get; set; }
	public double Longitude { get; set; }
	public double Latitude { get; set; }
	public string LocationArabicDescription { get; set; }
	public string LocationDescription { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<ClientOrder, GetTodayDashboardUnpaidDto>()
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

			.ForMember(d => d.LocationId,
				opts => opts.MapFrom(s => s.LocationId))
			.ForMember(d => d.LocationName,
				opts => opts.MapFrom(s => s.Location.Name))
			.ForMember(d => d.LocationArabicName,
				opts => opts.MapFrom(s => s.Location.ArabicName))
			.ForMember(d => d.LocationDescription,
				opts => opts.MapFrom(s => s.Location.Description))
			.ForMember(d => d.LocationArabicDescription,
				opts => opts.MapFrom(s => s.Location.ArabicDescription))
			.ForMember(d => d.LocationUrl,
				opts => opts.MapFrom(s => s.Location.Url))
			.ForMember(d => d.Longitude,
				opts => opts.MapFrom(s => s.Location.Longitude))
			.ForMember(d => d.Latitude,
				opts => opts.MapFrom(s => s.Location.Latitude))

			;
	}


}
