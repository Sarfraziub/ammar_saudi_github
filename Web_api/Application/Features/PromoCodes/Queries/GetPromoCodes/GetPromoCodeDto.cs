using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;
using Domain.Enum;

namespace Application.Features.PromoCodes.Queries.GetPromoCodes;

public class GetPromoCodeDto : IMapFrom<PromoCode>
{
	public long Id { get; set; }
	public string Code { get; set; }
	public DateTime? Expiry { get; set; }
	public decimal Percentage { get; set; }
	public bool AllowUpdate { get; set; }
    public PromoCodeApplicableType ApplicableType { get; set; }

    public void Mapping(Profile profile)
	{
		profile.CreateMap<PromoCode, GetPromoCodeDto>()
			.ForMember(d => d.AllowUpdate,
				opts => opts.MapFrom(s => !s.ClientOrders.Where(k=>k.Active == 1).Any()))
            ;
	}
}


