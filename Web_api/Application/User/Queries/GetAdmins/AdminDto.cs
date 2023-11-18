using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;

namespace Application.User.Queries.GetAdmins;

public class AdminDto : IMapFrom<ApplicationUser>
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }
	public string PhoneNumber { get; set; }
	public bool Locked { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<ApplicationUser, AdminDto>()
			.ForMember(d => d.Locked, opts
				=> opts.MapFrom(s => s.LockoutEnd != null))
			;
	}
}


