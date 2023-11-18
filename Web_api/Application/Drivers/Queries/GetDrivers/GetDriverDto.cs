using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;

namespace Application.Drivers.Queries.GetDrivers;

public class GetDriverDto : IMapFrom<ApplicationUser>
{
	public long DriverId { get; set; }
	public string Name { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }
	public string PhoneNumber { get; set; }
	public bool Locked { get; set; }
	public string Iban { get; set; }
	public string NationalId { get; set; }
	public string BankName { get; set; }
	public DateTime? ActivatedDate { get; set; }
	public bool? ActiveDriver { get; set; }


	public void Mapping(Profile profile)
	{
		profile.CreateMap<ApplicationUser, GetDriverDto>()
			.ForMember(d => d.DriverId, opts
				=> opts.MapFrom(s => s.Id))
			.ForMember(d => d.Locked, opts
				=> opts.MapFrom(s => s.LockoutEnd != null))
			;
	}
}
