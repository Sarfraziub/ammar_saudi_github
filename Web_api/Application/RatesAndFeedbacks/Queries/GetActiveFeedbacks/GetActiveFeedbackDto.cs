using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;

namespace Application.RatesAndFeedbacks.Queries.GetActiveFeedbacks;

public class GetActiveFeedbackDto : IMapFrom<ClientOrder>
{
	public long Id { get; set; }
	public Rates? Rate { get; set; }
	public string Feedback { get; set; }
	public bool? HideFeedback { get; set; }
	public string ClientName { get; set; }
	public string DriverName { get; set; }
	public DateTime Created { get; set; }
	public DateTime? DeliveryTime { get; set; }


	public void Mapping(Profile profile)
	{
		profile.CreateMap<ClientOrder, GetActiveFeedbackDto>()
			.ForMember(d => d.ClientName,
				opts => opts.MapFrom(s => s.Client != null ? s.Client.Name : null))

			.ForMember(d => d.DriverName,
				opts => opts.MapFrom(s => s.Client != null ? s.Driver.Name : null))

			;
	}
}

