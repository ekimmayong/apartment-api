using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using MountHebronAppApi.Context;
using MountHebronAppApi.Contracts;
using MountHebronAppApi.Mapper;
using MountHebronAppApi.Models;
using MountHebronAppApi.Utilities;


namespace MountHebronAppApi.Services
{
    public class PropertyService : IPropertyService
    {
        private const string BlobStorageName = "apartment-images";
        private const string BlogStorageName = "blog-images";
        private readonly PropertyContext _context;
        private readonly IPropertyMapper _map;
        private readonly IConfiguration _configuration;
        private readonly IStorageServices _storageServices;

        public PropertyService(PropertyContext context, IPropertyMapper map, IConfiguration configuration, IStorageServices storageServices)
        {
            _context = context;
            _map = map;
            _configuration = configuration;
            _storageServices = storageServices;
        }

        //Connect to Azure Storage Blobs
        public BlobContainerClient GetblobServiceClient(string storageName)
        {
            BlobServiceClient blobServiceClient = new(_configuration.GetConnectionString("AzureStorageConnectionString"));

            var blobClient = blobServiceClient.GetBlobContainerClient(storageName);

            return blobClient;

        }

        //Apartment
        public async Task AddNewApartment(ApartmentRequest request)
        {
            if (request.File != null)
            {
                Stream stream = request.File.OpenReadStream();

                var imageUri = await _storageServices.UploadDocument(request.File.FileName, stream, BlogStorageName);

                if (imageUri != null)
                {
                    var response = _map.NewApartment(request, imageUri);

                    await _context.AddAsync(response);

                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<ApartmentResponse>> GetApartments()
        {
            var apartment = await _context.Apartments.AsNoTracking().ToListAsync();
            var category = await _context.Categories.AsNoTracking().ToListAsync();

            return _map.GetApartments(apartment, category);
        }

        public async Task<ApartmentResponse> GetApartment(Guid uid)
        {
            var response = await GetApartments();

            return response.FirstOrDefault(a => a.Uid == uid);
        }

        public async Task DeleteApartment(Guid uid)
        {
            int id = GuidConversion.Guid2Int(uid);
            var response = await GetApartments();

            var dataId = response.Where(a => a.Uid == uid).FirstOrDefault();
        }

        //Blogs
        public Task<BlogRequest> AddNewBlog(BlogRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogResponse>> GetBlogs()
        {
            throw new NotImplementedException();
        }

        public Task<BlogResponse> GetBlog()
        {
            throw new NotImplementedException();
        }

        public Task DeleteBlog(Guid uid)
        {
            throw new NotImplementedException();
        }

        //Category
        public Task<CategoryRequest> AddNewCategory(CategoryRequest request)
        {
            throw new NotImplementedException();
        }



        public Task<IEnumerable<CategoryResponse>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryResponse> GetCategory()
        {
            throw new NotImplementedException();
        }

        //JoinMembers
        //Add Requested member
        public Task<MemberRequest> NewMembers(MemberRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MemberResponse>> GetMembers()
        {
            throw new NotImplementedException();
        }

        public Task<MemberResponse> GetMember(Guid uid)
        {
            throw new NotImplementedException();
        }


        //Join TEam
        //Create Request to Join membership
        public async Task<JoinRequest> AddNewJoin(JoinRequest model)
        {
            var response = _map.AddNewMemberRequest(model);

            await _context.AddAsync(response);
            await _context.SaveChangesAsync();

            return model;
        }
    }
}
