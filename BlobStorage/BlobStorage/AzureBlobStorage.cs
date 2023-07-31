using Azure.Storage.Blobs;

namespace BlobStorage
{
    internal class AzureBlobStorage
    {
        // Update the following 2 values
        private readonly string _storageAccount = "";
        private readonly string _accessKey = "";
        private readonly BlobServiceClient _blobServiceClient;
        private const string ContainerName = "files";
        private const string TodayFile = "test.txt";
        private const string TomorrowFile = "test.pdf";
        private const string TodayBlobPath = $"today/{TodayFile}";
        private const string TomorrowBlobPath = $"tomorrow/{TomorrowFile}";
        public AzureBlobStorage()
        {
            var credential = new Azure.Storage.StorageSharedKeyCredential(_storageAccount, _accessKey);
            var blobUrl = $"https://{_storageAccount}.blob.core.windows.net";
            _blobServiceClient = new BlobServiceClient(new Uri(blobUrl), credential);

        }

        public async Task ListBlobContainersAsync()
        {
            var containers = _blobServiceClient.GetBlobContainersAsync();

            await foreach(var container in containers)
            {
                Console.WriteLine(container.Name);
            }
        }

        public async Task<List<Uri>> UploadFilesAsync()
        {
            var blobUris = new List<Uri>();

            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            // TodayBlobPath = "today/test.txt"
            var todayBlobClient = blobContainerClient.GetBlobClient(TodayBlobPath);
            // TomorrowBlobPath = "tomorrow/test.pdf"
            var tomorrowBlobClient = blobContainerClient.GetBlobClient(TomorrowBlobPath);

            // TodayFile physical path to file on disk
            // true = override if file already exists, false will result in exception being thrown
            await todayBlobClient.UploadAsync(TodayFile, true);
            blobUris.Add(todayBlobClient.Uri);

            await tomorrowBlobClient.UploadAsync(TomorrowFile, true);
            blobUris.Add(tomorrowBlobClient.Uri);

            return blobUris;
        }

        public async Task GetFlatBlobListAsync()
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            var blobs = blobContainerClient.GetBlobsAsync();

            await foreach(var blob in blobs)
            {
                Console.WriteLine($"Blob Name = {blob.Name}");
            }
        }

        public async Task GetHierarchialBlobListAsync()
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            var blobs = blobContainerClient.GetBlobsByHierarchyAsync();

            await foreach (var blob in blobs)
            {
                if(blob.IsPrefix)
                {
                    // Write out the prefix of the Virtual Dirctory
                    Console.WriteLine($"Virtual Directory Prefix {blob.Prefix}");

                    // Call Recursively With The Prefix To Traverse The Virtual Directory
                    await GetFlatBlobListAsync();
                }
                else
                {
                    Console.WriteLine($"Blob Name = {blob.Prefix}");
                }
            }
        }

        public async Task DeleteFileAsync()
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            var blob = blobContainerClient.GetBlobClient(TodayBlobPath);

            await blob.DeleteIfExistsAsync();
        }

    }
}
