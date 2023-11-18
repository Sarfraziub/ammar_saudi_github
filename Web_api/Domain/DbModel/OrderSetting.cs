namespace Domain.DbModel;

public class OrderSetting : Entity
{
	public OrderSettingTypes OrderSettingType { get; set; }
	public decimal Value { get; set; }
}