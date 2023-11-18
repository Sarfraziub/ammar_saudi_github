using Application.Features.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructures.Storage.DiskStorage;

public class DiskStorageService : IImageStorageService
{
	private readonly HttpContext _httpContext;
	private readonly string _imagesPath;
	private readonly IWebHostEnvironment _webHostEnvironment;

	public DiskStorageService(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
	{
		_webHostEnvironment = webHostEnvironment;
		_httpContext = httpContextAccessor.HttpContext;
		_imagesPath = "wwwroot/uploads/";
	}

	public async Task<string> UploadFile(IFormFile photo, string fileName, CancellationToken cancellationToken)
	{
		const string path = "uploads/";
		var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

		if (!Directory.Exists(uploadPath))
			Directory.CreateDirectory(uploadPath);

		// var fileName = Guid.NewGuid() + Path.GetExtension(photo.FileName);
		var filePath = Path.Combine(uploadPath, fileName);

		await using var stream = File.Create(filePath);
		await photo.CopyToAsync(stream, cancellationToken);
		return fileName;
	}

	public string GetImageURL(string fileName)
	{
		return $"{_httpContext.Request.Scheme}://{_httpContext.Request.Host}/{_imagesPath}";
	}

	public Task<string> GetImageURL(long id)
	{
		throw new NotImplementedException();
	}

	public Task DownloadFile(string uri)
	{
		throw new NotImplementedException();
	}

    public Task<string> UploadFile(MemoryStream stream, string fileName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}


