using MediatR;

namespace Application.HomePageIcons.Commands.UpdateHomePageIcon;

public class UpdateHomePageIconCommand : IRequest<Unit>
{
	public long Id { get; set; }
	public string Title { get; set; }
	public string ArabicTitle { get; set; }
	// public long FileId { get; set; }
	public int Order { get; set; }
	public bool Visible { get; set; }
}
