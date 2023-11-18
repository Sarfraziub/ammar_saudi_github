using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;

namespace Application.UserNotifications.Queries.GetUserNotifications;

public class GetUserNotificationDto : IMapFrom<UserNotification>
{
	public long Id { get; set; }
	public long UserId { get; set; }
	public string Title { get; set; }
	public string Body { get; set; }

	public string ArabicTitle { get; set; }
	public string ArabicBody { get; set; }
	public DateTime Created { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<UserNotification, GetUserNotificationDto>();
	}
}
