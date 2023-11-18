namespace Domain.DbModel;

public class SliderItem : Entity
{
	public string Name { get; set; }
	public File Image { get; set; }
	public long ImageId { get; set; }
	public bool Visible { get; set; }
	public int Order { get; set; }
}


