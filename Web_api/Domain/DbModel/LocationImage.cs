namespace Domain.DbModel;

public class LocationImage : Entity
{
	public File File { get; set; }
	public long FileId { get; set; }

	public Location Location { get; set; }
	public long LocationId { get; set; }
}
