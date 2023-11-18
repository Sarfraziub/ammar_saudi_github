using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;

namespace Application.SliderItems.Queries.GetSliderItemById;

public class GetSliderItemByIdDto : IMapFrom<SliderItem>
{
	public long Id { get; set; }
	public string Name { get; set; }
	public long ImageId { get; set; }
	public string ImageUrl { get; set; }
	public bool Visible { get; set; }
	public int Order { get; set; }
	public DateTime Created { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<SliderItem, GetSliderItemByIdDto>();
	}
}


