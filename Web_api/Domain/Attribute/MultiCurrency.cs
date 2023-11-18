
namespace Domain.Attribute
{

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public sealed class MultiCurrency : System.Attribute
    {
        public MultiCurrency()
        {

        }
    }
}
