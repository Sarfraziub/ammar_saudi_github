using Application.Features.Common.Mappings;
using AutoMapper;
using Domain.DbModel;

namespace Application.Features.Clients.Queries.GetClients;

public class GetClientDto : IMapFrom<ApplicationUser>
{
	public long ClientId { get; set; }
	public string Name { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }
	public string PhoneNumber { get; set; }
	public bool Locked { get; set; }
	public int TotalOrders { get; set; }

	public void Mapping(Profile profile)
	{
		// profile.CreateMap<Domain.ApplicationUser, GetClientDto>()
		// 	.ForMember(d => d.ClientId, opts
		// 		=> opts.MapFrom(s => s.Id))

		profile.CreateMap<ApplicationUser, GetClientDto>()
			.ForMember(d => d.ClientId, opts
				=> opts.MapFrom(s => s.Id))
			.ForMember(d => d.Locked, opts
				=> opts.MapFrom(s => s.LockoutEnd != null))
			.ForMember(d => d.TotalOrders, opts
				=> opts.MapFrom(s => s.ClientOrders.Where(w => w.Active == 1).Count()))
			;
	}
}


