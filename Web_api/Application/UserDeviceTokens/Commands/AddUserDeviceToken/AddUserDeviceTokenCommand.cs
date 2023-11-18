using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.UserDeviceTokens.Commands.AddUserDeviceToken;

public class AddUserDeviceTokenCommand : IRequest<Unit>, IMapFrom<UserDeviceToken>
{
	public DeviceTypes DeviceType { get; set; }
	public string RegistrationToken { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<AddUserDeviceTokenCommand, UserDeviceToken>()
			;
	}
}


