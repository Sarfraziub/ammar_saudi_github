using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;

namespace Application.Countries.Queries.GetCountries;

public class GetCountryDto : IMapFrom<Country>
{
	public long Id { get; set; }
	public string Abbreviation { get; set; }
	public string Name { get; set; }
	public string ArabicName { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<Country, GetCountryDto>();
	}
}

