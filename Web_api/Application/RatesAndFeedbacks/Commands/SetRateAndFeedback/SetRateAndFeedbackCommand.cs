using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.RatesAndFeedbacks.Commands.SetRateAndFeedback;

public class SetRateAndFeedbackCommand : IRequest<Unit>
{
	public long ClientOrderId { get; set; }
	public Rates Rate { get; set; }
	public string Feedback { get; set; }
}


