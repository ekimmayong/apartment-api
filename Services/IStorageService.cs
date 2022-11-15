using MountHebronAppApi.Contracts;
using MountHebronAppApi.Models;

namespace MountHebronAppApi.Services
{
    public interface IStorageServices
    {
        //Azure Data Tables for Apartment
        Task<IEnumerable<ApartmentsResponse>> GetEntityAsync(string category);
        Task<ApartmentsResponse> GetEntityByIdAsync(string category, string id);
        Task<ApartmentModel> UpsertEntityAsync(ApartmentsRequest entity, string imageUri);
        Task DeleteEntityAsync(string category, string id, string blobStorage);

        //Azure Storage Blobs for Apartment
        Task<List<string>> GetAllDocuments();
        Task<string> UploadDocument(string fileName, Stream fileContent, string blobStorage);
        Task<string> GetDocument(string fileName);
        Task<bool> DeleteDocument(string fileName, string blobStorage);

        //Azure Data Table for Blogs
        Task<IEnumerable<BlogsResponse>> GetBlogs(string category);
        Task<BlogsResponse> GetBlogsById(string category, string id);
        Task<BlogModel> InsetNewBlog(BlogsRequest entity, string imageUri);
        Task DeleteBlog(string category, string id, string blobStorage);
    }
}