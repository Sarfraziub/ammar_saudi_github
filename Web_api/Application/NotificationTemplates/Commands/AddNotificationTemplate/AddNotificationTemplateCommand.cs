using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.NotificationTemplates.Commands.AddNotificationTemplate;

public class AddNotificationTemplateCommand : IRequest<Unit>, IMapFrom<NotificationTemplate>
{
	public string Title { get; set; }
	public string Body { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<AddNotificationTemplateCommand, NotificationTemplate>()
			;
	}
}


