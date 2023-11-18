using Microsoft.AspNetCore.Http;

namespace Application.Features.Common.Interfaces;

public interface IImageStorageService
{
	Task<string> UploadFile(IFormFile photo, string fileName, CancellationToken cancellationToken);
	Task<string> UploadFile(MemoryStream stream, string fileName, CancellationToken cancellationToken);
	string GetImageURL(string fileName);
	Task<string> GetImageURL(long id);
}


