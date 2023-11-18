using Application.Features.Common.Mappings;
using AutoMapper;
using Domain.DbModel;

namespace Application.Features.Common.Models.Firebase;

public class FirebaseMessage : IMapFrom<NotificationTemplate>
{
	public string Title { get; set; }
	public string Body { get; set; }

	public string ArabicTitle { get; set; }
	public string ArabicBody { get; set; }

	public Dictionary<string, string> Data { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<NotificationTemplate, FirebaseMessage>()
			;
	}
	// public string Topic { get; set; }
}


