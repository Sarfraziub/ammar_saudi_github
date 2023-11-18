
namespace Domain.DbModel;

public class HomePageIcon : Entity
{
	public string Title { get; set; }
	public string ArabicTitle { get; set; }
	public File File { get; set; }
	public long? FileId { get; set; }
	public int Order { get; set; }
	public bool Visible { get; set; }
}
