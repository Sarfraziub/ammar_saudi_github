using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Files.Queries.GetPhotoUrl;

public class Handler : IRequestHandler<GetFileUrlQuery, string>
{
	private readonly IApplicationDbContext _context;
	private readonly IErrorMessagesService _errorMessagesService;
	private readonly IImageStorageService _imageStorageService;


	public Handler(IApplicationDbContext context, IImageStorageService imageStorageService,
		IErrorMessagesService errorMessagesService)
	{
		_context = context;
		_imageStorageService = imageStorageService;
		_errorMessagesService = errorMessagesService;
	}

	public async Task<string> Handle(GetFileUrlQuery request, CancellationToken cancellationToken)
	{
		var file = await _context.Files
				.AsNoTracking()
				.Where(c => c.Id == request.Id && c.Active == 1)
				.SingleOrDefaultAsync(cancellationToken)
			;
		if (file == null) throw new AppBadRequestException(_errorMessagesService.GetCommonErrorMessageById(1));

		var fullPath = _imageStorageService.GetImageURL(file.Name);
		return fullPath;
	}
}


