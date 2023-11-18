using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;

namespace Application.NotificationTemplates.Queries.GetNotificationTemplates;

public class GetNotificationTemplateDto : IMapFrom<NotificationTemplate>
{
	public long Id { get; set; }
	public string Title { get; set; }
	public string Body { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<NotificationTemplate, GetNotificationTemplateDto>();
	}
}


