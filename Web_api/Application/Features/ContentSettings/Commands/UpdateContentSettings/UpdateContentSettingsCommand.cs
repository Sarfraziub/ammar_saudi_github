using Application.Features.Common.Mappings;
using AutoMapper;
using Domain.DbModel;
using MediatR;

namespace Application.Features.ContentSettings.Commands.UpdateContentSettings;

public class UpdateContentSettingsCommand : IRequest<Unit>, IMapFrom<ContentSetting>
{
	public void Mapping(Profile profile)
	{
		profile.CreateMap<UpdateContentSettingsCommand, ContentSetting>();
	}
	public string Title { get; set; }
	public string Content { get; set; }
	public string ArabicContent { get; set; }
	public string Phone { get; set; }
	public string Email { get; set; }
	public string Address { get; set; }
	public string WhatsApp { get; set; }
	public string Facebook { get; set; }
	public string Instagram { get; set; }
	public string Twitter { get; set; }
	public string Snapchat { get; set; }
}
