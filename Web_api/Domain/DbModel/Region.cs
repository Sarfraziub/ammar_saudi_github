namespace Domain.DbModel;

public class Region : Entity
{
	public string Name { get; set; }
	public string ArabicName { get; set; }
	public ICollection<Location> Locations { get; set; }
}


