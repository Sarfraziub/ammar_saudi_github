using Application.Features.Common.Interfaces;
using FluentValidation;

namespace Application.Files.Commands.Upload;

public class Validator : AbstractValidator<UploadCommand>
{
	// private ByteSize _byteSize;
	public Validator(IErrorMessagesService errorMessagesService)
	{
		// var maxFileSize = ByteSize.FromKiloBytes(4000);

		// RuleFor(x => x.File.Length).NotNull()
		// 	.LessThanOrEqualTo(long.Parse(maxFileSize.Bytes.ToString(CultureInfo.InvariantCulture)))
		// 	.WithMessage(errorMessagesService.GetCommonErrorMessageById(2));

		// RuleFor(x => x.File.ContentType).NotNull().Must(x =>
		// 		x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png") || x.Equals("application/pdf"))
		// 	.WithMessage(errorMessagesService.GetCommonErrorMessageById(3));
	}
}


