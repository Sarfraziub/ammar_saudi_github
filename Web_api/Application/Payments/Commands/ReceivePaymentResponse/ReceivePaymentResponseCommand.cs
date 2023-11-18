using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.Payments.Commands.ReceivePaymentResponse;

public class ReceivePaymentResponseCommand : IRequest<Unit>, IMapFrom<PaymentResponse>
{
    public string TransactionReference { get; set; }
    public string ResponseCode { get; set; }
    public string ResponseMessage { get; set; }
    public string ResponseStatus { get; set; }
    public string AcquirerMessage { get; set; }
    public string AcquirerRrn { get; set; }
    public string CartId { get; set; }
    public string CustomerEmail { get; set; }
    public string Signature { get; set; }
    public string Token { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ReceivePaymentResponseCommand, PaymentResponse>()
            ;
    }
}


