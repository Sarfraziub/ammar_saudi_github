namespace Domain.DbModel;

public class Country : Entity
{
	public string Abbreviation { get; set; }
	public string Name { get; set; }
	public string ArabicName { get; set; }
	public ICollection<ApplicationUser> Users { get; set; }
}


