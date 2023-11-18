namespace Domain.DbModel;

public class InfluencerVideo : Entity
{
	public File File { get; set; }
	public long FileId { get; set; }
	public bool Visible { get; set; }
	public string Title { get; set; }
	public string Content { get; set; }
	public ContentTypes ContentType { get; set; }
}
