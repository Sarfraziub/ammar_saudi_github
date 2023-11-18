using Application.Features.Common.Models.Payments;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.Payments.Commands.RequestPaymentPage;

public class RequestPaymentPageCommand : IRequest<RequestPaytapPageResponse>
{
    public PaymentTypes PaymentType { get; set; }
    public int ClientOrderId { get; set; }
}


