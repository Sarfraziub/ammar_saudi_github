using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.SliderItems.Commands.AddSliderItem;

public class AddSliderItemCommand : IRequest<Unit>, IMapFrom<SliderItem>
{
	public string Name { get; set; }
	public long ImageId { get; set; }
	public bool Visible { get; set; }
	public int Order { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<AddSliderItemCommand, SliderItem>()
			;
	}
}


