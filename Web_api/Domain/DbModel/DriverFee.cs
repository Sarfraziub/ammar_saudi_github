using System.ComponentModel.DataAnnotations;

namespace Domain.DbModel;

public class DriverFee : Entity
{
    public FeeTypes FeeType { get; set; }
    public decimal Value { get; set; }
    [DataType(DataType.Date)]
    public DateTime? StartDate { get; set; }
    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }
    public bool IsOffer { get; set; }
    public ICollection<ClientOrder> ClientOrders { get; set; }
}