using Application.Features.Common.Mappings;
using AutoMapper;
using Domain.DbModel;
using Newtonsoft.Json;

namespace Application.Features.Common.Models.Payments;

public class PaymentPayload : IMapFrom<PaymentTry>
{
    public PaymentPayload()
    {
        ShippingDetails = new ShippingDetails();
        CustomerDetails = new CustomerDetails();

    }
    [JsonProperty("shipping_details")] public ShippingDetails ShippingDetails { get; set; }
    [JsonProperty("hide_shipping")] public bool HideShipping = true;

    //Your profile ID
    [JsonProperty("profile_id")] public int ProfileId { get; set; }

    //tran_type : (sale, auth ..etc)
    [JsonProperty("tran_type")] public string TransactionType { get; set; }

    //(ecom, moto, cont)
    [JsonProperty("tran_class")] public string TransactionClass { get; set; }

    //Description of the items/services
    [JsonProperty("cart_description")] public string CartDescription { get; set; }

    //Unique order reference
    [JsonProperty("cart_id")] public string OrderReferenceId { get; set; }

    //3 character currency code
    [JsonProperty("cart_currency")] public string Currency { get; set; }

    //The total amount due,
    [JsonProperty("cart_amount")] public float Amount { get; set; }
    [JsonProperty("return")] public string Return { get; set; }
    [JsonProperty("callback")] public string Callback { get; set; }

    [JsonProperty("tokenise")] public string Tokenise { get; set; } = "2";
    [JsonProperty("payment_methods")] public List<string> PaymentMethods { get; set; }
    //[JsonProperty("paypage_lang")] public string Language { get; set; }

    [JsonProperty("customer_details")] public CustomerDetails CustomerDetails { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentTry, PaymentPayload>()
            .ForPath(d => d.CustomerDetails.Name,
                opts => opts.MapFrom(s => "Qatarat App"))
            .ForPath(d => d.CustomerDetails.Email,
                opts => opts.MapFrom(s => "email@domain.com"))
            .ForPath(d => d.CustomerDetails.City,
                opts => opts.MapFrom(s => "sa"))
            .ForPath(d => d.CustomerDetails.Country,
                opts => opts.MapFrom(s => "SA"))
            .ForPath(d => d.CustomerDetails.State,
                opts => opts.MapFrom(s => "11"))
            .ForPath(d => d.CustomerDetails.Phone,
                opts => opts.MapFrom(s => "00966559105459"))
            .ForPath(d => d.CustomerDetails.Street,
                opts => opts.MapFrom(s => "saudi arabia"))
            .ForPath(d => d.CustomerDetails.ZipCode,
                opts => opts.MapFrom(s => "12345"))

            .ForPath(d => d.ShippingDetails.Name,
                opts => opts.MapFrom(s => "Qatarat App"))
            .ForPath(d => d.ShippingDetails.Email,
                opts => opts.MapFrom(s => "email@domain.com"))
            .ForPath(d => d.ShippingDetails.City,
                opts => opts.MapFrom(s => "sa"))
            .ForPath(d => d.ShippingDetails.Country,
                opts => opts.MapFrom(s => "SA"))
            .ForPath(d => d.ShippingDetails.State,
                opts => opts.MapFrom(s => "11"))
            .ForPath(d => d.ShippingDetails.Phone,
                opts => opts.MapFrom(s => "00966559105459"))
            .ForPath(d => d.ShippingDetails.Street,
                opts => opts.MapFrom(s => "saudi arabia"))
            .ForPath(d => d.ShippingDetails.ZipCode,
                opts => opts.MapFrom(s => "12345"))
            ;
    }
    //If set to true , this will enable the paypage to be displayed inside iFrame
    // [JsonProperty("framed")] public bool UserIFrame { get; set; }
    // [JsonProperty("framed_return_top")] public bool UserIFrameReturnTop { get; set; }
    // [JsonProperty("hide_shipping")] public bool HideShipping { get; set; }
}
