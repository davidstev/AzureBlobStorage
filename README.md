# AzureBlobStorage
Example app for CRUD operations against Azure Blob Storage

# Before Running The code
- login to portal.azure.com
- ensure you have a resource group set up
- Create a Storage Account this can contain lowercase and numbers only
- Access the storage account, click Access Keys on the menu on the left and copy the access key
- update the _storageAccount and _accessKey values in AzureBlobStorage.cs
- Ensure the Azure.Storage.Blobs NuGet package has been added

# Authentication Methods
There are 3 ways to authenticate against Blob Storage

## Access Key
This is the most insecure method of authentication where a key is stored in vault or similar and passed to authenticate.
This is the authentication method used by this example app.
        
        
## Azure AD
Create a Service Principle to access the Storage Account.You will also need to register the application.

## Shared Access Signatures
Allows granular access to the Storage Account. Access to specific areas, for specific people, for a specific time.

# Azure SDK
BlobServiceClient (Storage Account) is used to acccess the BlobContainerClient (Folders), which can then access the BlobClient (Files).


