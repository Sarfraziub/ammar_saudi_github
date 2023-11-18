using System.ComponentModel.DataAnnotations;

namespace Application.Features.Common.Validations;

public class CommercialRegistryValidation : ValidationAttribute
{
	public CommercialRegistryValidation(int length)
	{
		Length = length;
	}

	private int Length { get; }

	public override bool IsValid(object value)
	{
		var strValue = value.ToString(); // as string;
		if (string.IsNullOrEmpty(strValue)) return false;
		var len = strValue.Length;
		return len == Length;
	}

	//protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	//{
	//     var strValue = value as string;
	//    if (string.IsNullOrEmpty(strValue)) return new ValidationResult(ErrorMessage);
	//    var len = strValue.Length;
	//    return len != Length ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
	//}

	//public override bool IsValid(object value)
	//{
	//    var strValue = value as string;
	//    if (string.IsNullOrEmpty(strValue)) return false;
	//    var len = strValue.Length;
	//    //if(len != Length)
	//    return len == Length;
	//}
	//protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	//{
	//    return new ValidationResult("Something went wrong");
	//}
}

//public class CustomAttribute : ValidationAttribute
//{
//    private readonly string _other;
//    public CustomAttribute(string other)
//    {
//        _other = other;
//    }

//    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//    {
//        var property = validationContext.ObjectType.GetProperty(_other);
//        if (property == null)
//        {
//            return new ValidationResult(
//                $"Unknown property: {_other}"
//            );
//        }
//        var otherValue = property.GetValue(validationContext.ObjectInstance, null);

//        return !object.Equals(value, otherValue) ? new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName)) : null;
//    }
//}

//public class CustomAttribute1 : ValidationAttribute
//{
//    private readonly string _other;
//    public CustomAttribute1(string other)
//    {
//        _other = other;
//    }

//    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//    {
//        var property = validationContext.ObjectType.GetProperty(_other);
//        if (property == null)
//        {
//            return new ValidationResult(
//                $"Unknown property: {_other}"
//            );
//        }
//        var otherValue = property.GetValue(validationContext.ObjectInstance, null);

//        // at this stage you have "value" and "otherValue" pointing
//        // to the value of the property on which this attribute
//        // is applied and the value of the other property respectively
//        // => you could do some checks
//        if (!object.Equals(value, otherValue))
//        {
//            // here we are verifying whether the 2 values are equal
//            // but you could do any custom validation you like
//            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
//        }
//        return null;
//    }
//}

