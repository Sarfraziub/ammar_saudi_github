using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;

namespace Application.RatesAndFeedbacks.Queries.GetAllFeedbacks;

public class GetAllFeedbackDto : IMapFrom<ClientOrder>
{
	public long Id { get; set; }
    public string Number { get; set; }

    public Rates? Rate { get; set; }
	public string Feedback { get; set; }
	public bool? HideFeedback { get; set; }

	public string ClientName { get; set; }
	public string DriverName { get; set; }
	public DateTime Created { get; set; }
	public DateTime? DeliveryTime { get; set; }

    public decimal TotalAfterDiscount { get; set; }
    public decimal Total { get; set; }
    public decimal? Discount { get; set; }
    public string PromoCode { get; set; }
    public decimal? PromoCodePercentage { get; set; }
    public string UserName  { get; set; }
    public string? PhoneNumber  { get; set; }


    public void Mapping(Profile profile)
	{
		profile.CreateMap<ClientOrder, GetAllFeedbackDto>()
			.ForMember(d => d.ClientName,
				opts => opts.MapFrom(s => s.Client != null ? s.Client.Name : null))

			.ForMember(d => d.DriverName,
				opts => opts.MapFrom(s => s.Client != null ? s.Driver.Name : null))

            .ForMember(d => d.Total,
                opts =>
                    opts.MapFrom(s =>
                        s.ClientOrderDetails.Where(f => f.Active == 1).Sum(dd => dd.Price * dd.Quantity)))
            .ForMember(d => d.PromoCode,
                opts =>
                    opts.MapFrom(s => s.PromoCode != null ? s.PromoCode.Code : null))
            .ForMember(d => d.PromoCodePercentage,
                opts =>
                    opts.MapFrom(s => s.PromoCode != null ? s.PromoCode.Percentage : 0))
            .ForMember(d => d.TotalAfterDiscount,
                opts =>
                    opts.MapFrom(s =>
                        s.PromoCode != null
                            ? s.ClientOrderDetails.Where(d => d.Active == 1).Sum(f => f.Price * f.Quantity) -
                              s.ClientOrderDetails.Where(d => d.Active == 1).Sum(f => f.Price * f.Quantity) *
                              s.PromoCode.Percentage
                            : s.ClientOrderDetails.Where(d => d.Active == 1).Sum(f => f.Price * f.Quantity))
                    )
            .ForMember(d => d.UserName,
                opts => opts.MapFrom(s => s.Client != null ? s.Client.UserName : null))
            .ForMember(d => d.PhoneNumber,
                opts => opts.MapFrom(s => s.Client != null ? s.Client.PhoneNumber : null))
            ;
	}
}

