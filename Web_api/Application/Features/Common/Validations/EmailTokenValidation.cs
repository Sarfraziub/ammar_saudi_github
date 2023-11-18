using System.ComponentModel.DataAnnotations;

namespace Application.Features.Common.Validations;

public class EmailTokenValidation : ValidationAttribute
{
	public EmailTokenValidation(int length)
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
}

