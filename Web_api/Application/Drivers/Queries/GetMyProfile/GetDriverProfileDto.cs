using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;
using EnumStringValues;

namespace Application.Drivers.Queries.GetMyProfile;

public class GetDriverProfileDto : IMapFrom<ApplicationUser>
{
	public long DriverId { get; set; }
	public string Name { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }
	public string PhoneNumber { get; set; }
	public bool Locked { get; set; }
	public long? NationalImageImageId { get; set; }
	public long? IbanImageId { get; set; }
	public long? ImageId { get; set; }

	public string IbanUrl { get; set; }
	public string NationalIdUrl { get; set; }
	public string BankNameUrl { get; set; }
	public string ImageUrl { get; set; }

	public string Iban { get; set; }
	public string NationalId { get; set; }
	public string BankName { get; set; }
	public string CountryArabicName { get; set; }
	public string CountryName { get; set; }
	public string LanguageName { get; set; }
	public int TotalOrderDelivered { get; set; }
	public DateTime? ActivatedDate { get; set; }
	public bool? ActiveDriver { get; set; }


	public void Mapping(Profile profile)
	{
		profile.CreateMap<ApplicationUser, GetDriverProfileDto>()
			.ForMember(d => d.LanguageName, opts
				=> opts.MapFrom(s => s.Language == 0 ? null : s.Language.GetStringValue()))
			.ForMember(d => d.CountryName, opts
				=> opts.MapFrom(s => s.Country != null ? s.Country.Name : null))
			.ForMember(d => d.CountryArabicName, opts
				=> opts.MapFrom(s => s.Country != null ? s.Country.ArabicName : null))
			// .ForMember(d => d.TotalOrderDelivered, opts
			// 	=> opts.MapFrom(s =>
			// 		s.ClientOrders.Where(w => w.Active == 1 && w.ClientOrderStatus == ClientOrderStatuses.Delivered)
			// 			.Count()))
			;
	}
}
