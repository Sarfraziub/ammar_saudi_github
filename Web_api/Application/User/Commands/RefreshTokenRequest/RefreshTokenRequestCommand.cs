using Application.Features.Common.Mappings;
using AutoMapper;
using MediatR;

namespace Application.User.Commands.RefreshTokenRequest;

public class RefreshTokenRequestCommand : IRequest<LoginResult>, IMapFrom<RefreshTokenRequestModel>
{
	public string Username { get; set; }
	public string Role { get; set; }
	public string RefreshToken { get; set; }
	public string AccessToken { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<RefreshTokenRequestModel, RefreshTokenRequestCommand>()
			//.ForMember(d => d.RefreshToken, opts => opts.MapFrom(s => s.RefreshToken))
			;
	}
}


