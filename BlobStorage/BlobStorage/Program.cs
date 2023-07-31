// See https://aka.ms/new-console-template for more information

using BlobStorage;

var blobService = new AzureBlobStorage();
await blobService.ListBlobContainersAsync();
await blobService.UploadFilesAsync();
await blobService.GetFlatBlobListAsync();
Console.WriteLine("************");
await blobService.GetHierarchialBlobListAsync();
await blobService.DeleteFileAsync();

Console.ReadLine();
