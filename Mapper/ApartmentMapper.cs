using MountHebronAppApi.Contracts;
using MountHebronAppApi.Models;
using Newtonsoft.Json;

namespace MountHebronAppApi.Mapper
{
    public class ApartmentMapper : IApartmentMapper
    {
        public IEnumerable<ApartmentResponse> Apartments(IEnumerable<ApartmentModel> apartments)
        {
            return apartments.Select(Apartment);
        }


        //Return Apartment Data from Azure Storage Data
        public ApartmentResponse Apartment(ApartmentModel apartments)
        {
            return new ApartmentResponse()
            {
                PartitionKey = apartments.PartitionKey,
                RowKey = apartments.RowKey,
                Description = apartments.Description,
                Name = apartments.Name,
                Price = apartments.Price,
                Type = apartments.Type,
                Status = apartments.Status,
                ImageName = apartments.ImageName,
                PurchaseType = apartments.PurchaseType,
                ImageUri = apartments.ImageUri,
                Address = apartments.Address == null ? new AddressModel() : JsonConvert.DeserializeObject<AddressModel>(apartments.Address)
            };
        }

        //Add new data to Azure Storage Table
        public ApartmentModel NewApartment(ApartmentRequest request, string imageUri)
        {
            var id = Guid.NewGuid().ToString();
            var address = new AddressModel()
            {
                City = request.Address.City,
                Country = request.Address.Country,
                Province = request.Address.Province,
                Town = request.Address.Town,
                ZipCode = request.Address.ZipCode
            };

            return new ApartmentModel()
            {
                Id = id,
                Category = request.Category,
                Name = request.Name,
                Type = request.Type,
                Description = request.Description,
                ImageName = request.File.FileName,
                ImageUri = imageUri,
                Price = request.Price,
                Status = request.Status,
                PurchaseType = request.PurchaseType,
                PartitionKey = request.Category,
                RowKey = id,
                Address = JsonConvert.SerializeObject(address)
            };
        }

        //Blog Mappers
        public IEnumerable<BlogResponse> GetBlogs(IEnumerable<BlogModel> blog)
        {
            return blog.Select(GetBlog);
        }

        public BlogResponse GetBlog(BlogModel blog)
        {

            return new BlogResponse()
            {
                RowKey = blog.RowKey,
                PartitionKey = blog.PartitionKey,
                Message = blog.Message,
                BlogType = blog.BlogType,
                BloggerInfo = blog.BloggerInfo == null ? new BlogEntry() : JsonConvert.DeserializeObject<BlogEntry >(blog.BloggerInfo)
            };
        }

        public BlogModel NewBlog(BlogRequest request, string imageUrl)
        {
            var id = Guid.NewGuid().ToString();

            var bloggerInfo = new BlogEntry()
            {
                Email = request.BloggerInfo.Email,
                Company = request.BloggerInfo.Company,
                ImageUri = imageUrl,
                Name = request.BloggerInfo.Name,
                Position = request.BloggerInfo.Position,
                ImageName = request.File.FileName
            };

            return new BlogModel()
            {
                PartitionKey = request.Category,
                RowKey = id,
                Message = request.Message,
                BlogType = request.BlogType,
                BloggerInfo = JsonConvert.SerializeObject(bloggerInfo)
            };
        }
    }
}
