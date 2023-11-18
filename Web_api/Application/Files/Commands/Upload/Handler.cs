using Application.Features.Common.Interfaces;
using Application.Helper;
using AutoMapper;
using MediatR;
using File = Domain.DbModel.File;

namespace Application.Files.Commands.Upload;

public class Handler : IRequestHandler<UploadCommand, long>
{
	private readonly IApplicationDbContext _context;
	private readonly IImageStorageService _imageStorageService;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IImageStorageService imageStorageService, IMapper mapper)
	{
		_context = context;
		_imageStorageService = imageStorageService;
		_mapper = mapper;
	}

	public async Task<long> Handle(UploadCommand request, CancellationToken cancellationToken)
	{
		var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";
		
		if(ImageHelper.IsImage(request.File))
		{
			var resizedImage = await ImageHelper.ResizeImage(request.File.OpenReadStream(), 1024, 800);
            await _imageStorageService.UploadFile(resizedImage, fileName, cancellationToken);
        }
        else
		{
            await _imageStorageService.UploadFile(request.File, fileName, cancellationToken);
        }
		var entity = _mapper.Map<File>(request);
		entity.Name = fileName;
		_context.Files.Add(entity);
		await _context.SaveChangesAsync(cancellationToken);
		return entity.Id;
	}
}


