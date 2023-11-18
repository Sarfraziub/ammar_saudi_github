using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.Attribute;
using Domain.DbModel;
using EnumStringValues;

namespace Application.User.Queries.GetMyProfile;

public class GetMyProfileDto : IMapFrom<ApplicationUser>
{
	public string Name { get; set; }
	public string Email { get; set; }
	public long? ImageId { get; set; }
	public string ImageUrl { get; set; }
	public int OrdersCount { get; set; }
	[MultiCurrency]
	public decimal TotalSpending { get; set; }
	public Languages Language { get; set; }
	public string LanguageName { get; set; }
	public string Street { get; set; }
	public string City { get; set; }
	public string State { get; set; }
	public string CountryName { get; set; }
	public string CountryArabicName { get; set; }
	public long? CountryId { get; set; }
	public string ZipCode { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<ApplicationUser, GetMyProfileDto>()
			.ForMember(d => d.LanguageName, opts
				=> opts.MapFrom(s => s.Language == 0 ? null : s.Language.GetStringValue()))
			.ForMember(d => d.CountryName, opts
				=> opts.MapFrom(s => s.Country != null ? s.Country.Name : null))
			.ForMember(d => d.CountryArabicName, opts
				=> opts.MapFrom(s => s.Country != null ? s.Country.ArabicName : null))
			;
	}
}


