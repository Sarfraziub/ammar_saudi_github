using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

namespace Infrastructures.Storage.AzureStorage;

public static class StorageHelper
{
	public static async Task<bool> UploadFileToStorage(Stream fileStream, string fileName,
		AzureStorageConfig _storageConfig)
	{
		// Create a URI to the blob
		var blobUri = new Uri("https://" +
		                      _storageConfig.AccountName +
		                      ".blob.core.windows.net/" +
		                      _storageConfig.ImageContainer +
		                      "/" + fileName);

		// Create StorageSharedKeyCredentials object by reading
		// the values from the configuration (appsettings.json)
		var storageCredentials =
			new StorageSharedKeyCredential(_storageConfig.AccountName, _storageConfig.AccountKey);

		// Create the blob client.
		var blobClient = new BlobClient(blobUri, storageCredentials);

		// Upload the file
		await blobClient.UploadAsync(fileStream);

		return await Task.FromResult(true);
	}

	public static async Task<List<string>> GetThumbNailUrls(AzureStorageConfig _storageConfig)
	{
		var thumbnailUrls = new List<string>();

		// Create a URI to the storage account
		var accountUri = new Uri("https://" + _storageConfig.AccountName + ".blob.core.windows.net/");

		// Create BlobServiceClient from the account URI
		var blobServiceClient = new BlobServiceClient(accountUri);

		// Get reference to the container
		var container = blobServiceClient.GetBlobContainerClient(_storageConfig.ThumbnailContainer);

		if (container.Exists())
			foreach (var blobItem in container.GetBlobs())
				thumbnailUrls.Add(container.Uri + "/" + blobItem.Name);

		return await Task.FromResult(thumbnailUrls);
	}
}


