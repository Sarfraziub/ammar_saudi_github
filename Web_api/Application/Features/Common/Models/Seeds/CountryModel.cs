using Application.Features.Common.Mappings;
using AutoMapper;
using Domain.DbModel;

namespace Application.Features.Common.Models.Seeds;

public class CountryModel : IMapFrom<Country>
{
	public string alpha2 { get; set; }
	public string ar { get; set; }
	public string en { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<CountryModel, Country>()
			.ForMember(d => d.ArabicName,
				opts => opts.MapFrom(s => s.ar))
			.ForMember(d => d.Name,
				opts => opts.MapFrom(s => s.en))
			.ForMember(d => d.Abbreviation,
				opts => opts.MapFrom(s => s.alpha2))
			;
	}
}

