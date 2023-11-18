using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;

namespace Application.HomePageIcons.Queries.GetHomePageIcons;

public class GetHomePageIconDto : IMapFrom<HomePageIcon>
{
	public long Id { get; set; }
	public string Title { get; set; }
	public string ArabicTitle { get; set; }
	public long FileId { get; set; }
	public string ImageUrl { get; set; } = "";
	public int Order { get; set; }
	public bool Visible { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<HomePageIcon, GetHomePageIconDto>();
	}
}
