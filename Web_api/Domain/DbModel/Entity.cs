namespace Domain.DbModel;

public class Entity
{
	protected Entity()
	{
	}

	protected Entity(long id)
		: this()
	{
		Id = id;
	}
	//private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
	//public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

	public long Id { get; }
	public DateTime Created { get; set; }

	public string CreatedBy { get; set; }

	public DateTime? Updated { get; set; }

	public string UpdatedBy { get; set; }
	public int Active { get; set; }

	//protected void RaiseDomainEvent(IDomainEvent domainEvent)
	//{
	//    _domainEvents.Add(domainEvent);
	//}

	//public void ClearDomainEvents()
	//{
	//    _domainEvents.Clear();
	//}

	public override bool Equals(object obj)
	{
		if (!(obj is Entity other))
			return false;

		if (ReferenceEquals(this, other))
			return true;

		if (GetRealType() != other.GetRealType())
			return false;

		if (Id == 0 || other.Id == 0)
			return false;

		return Id == other.Id;
	}

	public static bool operator ==(Entity a, Entity b)
	{
		if (a is null && b is null)
			return true;

		if (a is null || b is null)
			return false;

		return a.Equals(b);
	}

	public static bool operator !=(Entity a, Entity b)
	{
		return !(a == b);
	}

	public override int GetHashCode()
	{
		return (GetRealType().ToString() + Id).GetHashCode();
	}

	private Type GetRealType()
	{
		var type = GetType();

		if (type.ToString().Contains("Castle.Proxies."))
			return type.BaseType;

		return type;
	}

    public interface IMultiCurrency
    {
        public long ChargedCurrencyId { get; set; }
        public decimal ChargedPrice { get; set; }
    }
}


