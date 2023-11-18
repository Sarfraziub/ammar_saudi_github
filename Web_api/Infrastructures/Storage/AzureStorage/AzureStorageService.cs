using Application.Features.Common.Interfaces;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructures.Storage.AzureStorage;

public class AzureStorageService : IImageStorageService
{
	private readonly AzureStorageConfig _config;
	private readonly IApplicationDbContext _context;

	public AzureStorageService(IOptions<AzureStorageConfig> config, IApplicationDbContext context)
	{
		_context = context;
		_config = config.Value;
	}

	public async Task<string> UploadFile(IFormFile photo, string fileName, CancellationToken cancellationToken)
	{
		// if (!StorageHelper.IsImage(photo)) throw new Exception("Not image");
		if (photo.Length <= 0) throw new Exception("No file !");
		// var fileName = Guid.NewGuid() + Path.GetExtension(photo.FileName);

		await using var stream = photo.OpenReadStream();
		var blobUri = new Uri("https://" +
		                      _config.AccountName +
		                      ".blob.core.windows.net/" +
		                      _config.ImageContainer +
		                      "/" + fileName);

		// Create StorageSharedKeyCredentials object by reading
		// the values from the configuration (appsettings.json)
		var storageCredentials =
			new StorageSharedKeyCredential(_config.AccountName, _config.AccountKey);

		// Create the blob client.
		var blobClient = new BlobClient(blobUri, storageCredentials);

		// Upload the file
		BlobContentInfo info = await blobClient.UploadAsync(stream, cancellationToken);

		return await Task.FromResult(fileName);
	}
	public async Task<string> UploadFile(MemoryStream stream, string fileName, CancellationToken cancellationToken)
	{
		if (stream.Length <= 0) throw new Exception("No file !");

		var blobUri = new Uri("https://" +
		                      _config.AccountName +
		                      ".blob.core.windows.net/" +
		                      _config.ImageContainer +
		                      "/" + fileName);

		var storageCredentials =
			new StorageSharedKeyCredential(_config.AccountName, _config.AccountKey);

		var blobClient = new BlobClient(blobUri, storageCredentials);
        stream.Seek(0, SeekOrigin.Begin);
        BlobContentInfo info = await blobClient.UploadAsync(stream, cancellationToken);

		return await Task.FromResult(fileName);
	}

	public string GetImageURL(string fileName)
	{
		return $"https://{_config.AccountName}.blob.core.windows.net/{_config.ImageContainer}/{fileName}";
	}

	public async Task<string> GetImageURL(long id)
	{
		var file = await _context.Files.FindAsync(id);
		return GetImageURL(file.Name);
	}

	// public static bool IsImage(IFormFile file)
	// {
	// 	if (file.ContentType.Contains("image"))
	// 	{
	// 		return true;
	// 	}
	//
	// 	var formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };
	//
	// 	return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
	// }
}


