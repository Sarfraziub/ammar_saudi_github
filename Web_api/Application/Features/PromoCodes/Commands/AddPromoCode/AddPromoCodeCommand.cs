using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;
using Domain.Enum;
using MediatR;

namespace Application.Features.PromoCodes.Commands.AddPromoCode;

public class AddPromoCodeCommand : IRequest<Unit>, IMapFrom<PromoCode>
{
	public string Code { get; set; }
	public DateTime? Expiry { get; set; }
	public decimal Percentage { get; set; }
    public PromoCodeApplicableType ApplicableType { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<AddPromoCodeCommand, PromoCode>()
			;
	}
}


