using MediatR;

namespace Application.RatesAndFeedbacks.Commands.ShowHideFeedback;

public class ShowHideFeedbackCommand : IRequest<Unit>
{
	public long ClientOrderId { get; set; }
	public bool HideFeedback { get; set; }
}
