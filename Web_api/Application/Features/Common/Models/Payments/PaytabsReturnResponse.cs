using Application.Features.Common.Mappings;
using Application.Payments.Commands.ReceivePaymentResponse;
using AutoMapper;
using Newtonsoft.Json;

namespace Application.Features.Common.Models.Payments;

public class PaytabsReturnResponse : IMapFrom<ReceivePaymentResponseCommand>
{
    [JsonProperty("tranRef")] public string tranRef { get; set; }
    [JsonProperty("respCode")] public string respCode { get; set; }
    [JsonProperty("respMessage")] public string respMessage { get; set; }
    [JsonProperty("respStatus")] public string respStatus { get; set; }
    [JsonProperty("acquirerMessage")] public string acquirerMessage { get; set; }
    [JsonProperty("acquirerRRN")] public string acquirerRRN { get; set; }
    [JsonProperty("cartId")] public string cartId { get; set; }
    [JsonProperty("customerEmail")] public string customerEmail { get; set; }
    [JsonProperty("signature")] public string signature { get; set; }
    [JsonProperty("token")] public string token { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaytabsReturnResponse, ReceivePaymentResponseCommand>()
            .ForMember(d => d.TransactionReference,
                opts => opts.MapFrom(s => s.tranRef))
            .ForMember(d => d.ResponseCode,
                opts => opts.MapFrom(s => s.respCode))
            .ForMember(d => d.ResponseMessage,
                opts => opts.MapFrom(s => s.respMessage))
            .ForMember(d => d.ResponseStatus,
                opts => opts.MapFrom(s => s.respStatus))
            .ForMember(d => d.AcquirerMessage,
                opts => opts.MapFrom(s => s.acquirerMessage))
            .ForMember(d => d.AcquirerRrn,
                opts => opts.MapFrom(s => s.acquirerRRN))
            .ForMember(d => d.CartId,
                opts => opts.MapFrom(s => s.cartId))
            .ForMember(d => d.CustomerEmail,
                opts => opts.MapFrom(s => s.customerEmail))
            .ForMember(d => d.Signature,
                opts => opts.MapFrom(s => s.signature))
            .ForMember(d => d.Token,
                opts => opts.MapFrom(s => s.token))
            ;
    }

    // [JsonProperty("tranRef")] public string TransactionReference { get; set; }
    // [JsonProperty("respCode")] public string ResponseCode { get; set; }
    // [JsonProperty("respMessage")] public string ResponseMessage	 { get; set; }
    // [JsonProperty("respStatus")] public string ResponseStatus { get; set; }
    // [JsonProperty("acquirerMessage")] public string AcquirerMessage { get; set; }
    // [JsonProperty("acquirerRRN")] public string AcquirerRrn { get; set; }
    // [JsonProperty("cartId")] public string CartId { get; set; }
    // [JsonProperty("customerEmail")] public string CustomerEmail { get; set; }
    // [JsonProperty("signature")] public string Signature { get; set; }
    // [JsonProperty("token")] public string Token { get; set; }
}


