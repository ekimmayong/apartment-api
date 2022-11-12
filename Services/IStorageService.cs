using MountHebronAppApi.Contracts;
using MountHebronAppApi.Models;
using System.IO;

namespace MountHebronAppApi.Services
{
    public interface IStorageServices
    {
        //Azure Data Tables for Apartment
        Task<IEnumerable<ApartmentResponse>> GetEntityAsync(string category);
        Task<ApartmentResponse> GetEntityByIdAsync(string category, string id);
        Task<ApartmentModel> UpsertEntityAsync(ApartmentRequest entity, string imageUri);
        Task DeleteEntityAsync(string category, string id, string blobStorage);

        //Azure Storage Blobs for Apartment
        Task<List<string>> GetAllDocuments();
        Task<string> UploadDocument(string fileName, Stream fileContent, string blobStorage);
        Task<string> GetDocument(string fileName);
        Task<bool> DeleteDocument(string fileName, string blobStorage);

        //Azure Data Table for Blogs
        Task<IEnumerable<BlogResponse>> GetBlogs(string category);
        Task<BlogResponse> GetBlogsById(string category, string id);
        Task<BlogModel> InsetNewBlog(BlogRequest entity, string imageUri);
        Task DeleteBlog(string category, string id, string blobStorage);
    }
}