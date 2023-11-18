using MediatR;

namespace Application.RatesAndFeedbacks.Queries.GetAllFeedbacks;

public class GetAllFeedbacksQuery : IRequest<GetAllFeedbacksVm>
{
	public bool? HideFeedback { get; set; }
}


