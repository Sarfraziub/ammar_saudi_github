namespace Domain.DbModel;

public class Package : Entity
{
	public string Name { get; set; }
	public string Description { get; set; }

	public string ArabicName { get; set; }
	public string ArabicDescription { get; set; }

	public bool Visible { get; set; }
	public File File { get; set; }
	public long? FileId { get; set; }
	public ICollection<SaleItem> SaleItems { get; set; }
}
