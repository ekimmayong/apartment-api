using Azure;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MountHebronAppApi.Contracts;
using MountHebronAppApi.Mapper;
using MountHebronAppApi.Models;

namespace MountHebronAppApi.Services
{
    public class StorageService : IStorageServices
    {
        private const string TableName = "apartments";
        private const string BlogTableName = "blogs";
        private const string BlobStorageName = "apartment-images";
        private const string BlogStorageName = "blog-images";
        private readonly IConfiguration _configuration;
        private readonly IApartmentMapper _map;
        public StorageService(IConfiguration configuration, IApartmentMapper map)
        {
            _configuration = configuration;
            _map = map;
        }
        //-------------------- Storae Connection Service

        //Connect to Azure Storage Table
        private async Task<TableClient> GetTableClient(string tableName)
        {
            var serviceClient = new TableServiceClient(_configuration.GetConnectionString("AzureStorageConnectionString"));
            var tableClient = serviceClient.GetTableClient(tableName);
            await tableClient.CreateIfNotExistsAsync();
            return tableClient;
        }

        //Connect to Azure Storage Blobs
        public BlobContainerClient GetblobServiceClient(string storageName)
        {
            BlobServiceClient blobServiceClient = new(_configuration.GetConnectionString("AzureStorageConnectionString"));

            var blobClient = blobServiceClient.GetBlobContainerClient(storageName);

            return blobClient;

        }

        //---------------------End of Storage Connection service

        //Azure Storage Table Section
        public async Task<IEnumerable<ApartmentResponse>> GetEntityAsync(string category)
        {
            var tableClient = await GetTableClient(TableName);

            Pageable<ApartmentModel> apartments = tableClient.Query<ApartmentModel>(a => a.PartitionKey == category);

            var response = _map.Apartments(apartments);

            return response;
        }

        public async Task<ApartmentResponse> GetEntityByIdAsync(string category, string id)
        {
            var tableClient = await GetTableClient(TableName);

            var apartment =  await tableClient.GetEntityAsync<ApartmentModel>(category, id);

            var response = _map.Apartment(apartment);

            return response;
        }


        public async Task<ApartmentModel> UpsertEntityAsync(ApartmentRequest entity, string imageUri)
        {
            var apartmentRequest = _map.NewApartment(entity, imageUri);

            var tableClient = await GetTableClient(TableName);

            await tableClient.UpsertEntityAsync(apartmentRequest);
            return apartmentRequest;
        }


        public async Task DeleteEntityAsync(string category, string id, string blobStorage)
        {
            var tableClient = await GetTableClient(TableName);

            var apartment = await tableClient.GetEntityAsync<ApartmentModel>(category, id);

            //Delete Image from the Blob
            await DeleteDocument(apartment.Value.ImageName, blobStorage);
            //Delete Items from the Tables
            await tableClient.DeleteEntityAsync(category, id);            
        }

        //Azure Storage Blobs Section
        //Retrieve all document from the blob
        public async Task<List<string>> GetAllDocuments()
        {
            var container = GetblobServiceClient(BlobStorageName);
            if(!await container.ExistsAsync())
            {
                return new List<string>();
            }

            List<string> blobs = new();

            await foreach(BlobItem blobIten in container.GetBlobsAsync())
            {
                blobs.Add(blobIten.Name);
            }
            return blobs;
        }


        public async Task<string> UploadDocument(string fileName, Stream fileContent, string blobStorage)
        {
            string fileUri = "";
            var container = GetblobServiceClient(blobStorage);
            if (!await container.ExistsAsync())
            {
                BlobServiceClient blobServiceClient = new(_configuration.GetConnectionString("AzureStorageConnectionString"));
                await blobServiceClient.CreateBlobContainerAsync(blobStorage);
                container = blobServiceClient.GetBlobContainerClient(blobStorage);
            }

            var blobclient = container.GetBlobClient(fileName);
            var blobContentType = new BlobHttpHeaders { };

            if(Path.GetExtension(blobclient.Uri.AbsoluteUri) == ".png")
            {
                blobContentType = new BlobHttpHeaders() { ContentType = "image/png" };
            }
            else if(Path.GetExtension(blobclient.Uri.AbsoluteUri) == ".jpg" || Path.GetExtension(blobclient.Uri.AbsoluteUri) == ".jpeg")
            {
                blobContentType = new BlobHttpHeaders() { ContentType = "image/jpg" };
            }

            if (!blobclient.Exists())
            {
                fileContent.Position = 0;
                await container.UploadBlobAsync(fileName, fileContent);
                blobclient.SetHttpHeaders(blobContentType);
                //await blobclient.UploadAsync(fileContent, new BlobUploadOptions { HttpHeaders = blobContentType});
                fileUri = blobclient.Uri.OriginalString;
            }
            else
            {
                fileContent.Position = 0;
                await blobclient.UploadAsync(fileContent, overwrite: true);
                blobclient.SetHttpHeaders(blobContentType);
                fileUri = blobclient.Uri.OriginalString;
            }

            return fileUri;
        }


        public async Task<string> GetDocument(string fileName)
        {
            string fileUri = "";
            var container = GetblobServiceClient(BlobStorageName);
            if (await container.ExistsAsync())
            {
                var blobClient = container.GetBlobClient(fileName);
                if (blobClient.Exists())
                {
                    fileUri = blobClient.Uri.OriginalString.ToString();
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
            return fileUri;
        }


        public async Task<bool> DeleteDocument(string fileName, string blobStorage)
        {
            var container = GetblobServiceClient(blobStorage);
            if (!await container.ExistsAsync())
            {
                return false;
            }

            var blobClient = container.GetBlobClient(fileName);

            if (await blobClient.ExistsAsync())
            {
                await blobClient.DeleteIfExistsAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Blog Services Entry
        public async Task<IEnumerable<BlogResponse>> GetBlogs(string category)
        {
            var tableClient = await GetTableClient(BlogTableName);

            Pageable<BlogModel> blogs = tableClient.Query<BlogModel>(a => a.PartitionKey == category);

            var response = _map.GetBlogs(blogs);

            return response;
        }

        public async Task<BlogResponse> GetBlogsById(string category, string id)
        {
            var tableClient = await GetTableClient(BlogTableName);

            var blog = await tableClient.GetEntityAsync<BlogModel>(category, id);

            var response = _map.GetBlog(blog);

            return response;
        }

        public async Task<BlogModel> InsetNewBlog(BlogRequest entity, string imageUri)
        {
            var blogRequest = _map.NewBlog(entity, imageUri);

            var tableClient = await GetTableClient(BlogTableName);

            await tableClient.UpsertEntityAsync(blogRequest);

            return blogRequest;
        }

        public async Task DeleteBlog(string category, string id, string blobStorage)
        {
            var tableClient = await GetTableClient(BlogTableName);

            var blog = await tableClient.GetEntityAsync<BlogModel>(category, id);

            //map blog data
            var mapBlog = _map.GetBlog(blog);

            //Delete Image from the Blob
            await DeleteDocument(mapBlog.BloggerInfo.ImageName, blobStorage);

            //Delete Items from the Tables
            await tableClient.DeleteEntityAsync(category, id);
        }
    }
}