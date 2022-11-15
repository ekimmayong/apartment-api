using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using MountHebronAppApi.Contracts;
using MountHebronAppApi.Models;
using MountHebronAppApi.Utilities;
using Newtonsoft.Json;

namespace MountHebronAppApi.Mapper
{
    public class PropertyMapper : IPropertyMapper
    {
        

        public IEnumerable<ApartmentsResponse> Apartments(IEnumerable<ApartmentModel> apartments)
        {
            return apartments.Select(Apartment);
        }

        //Return Apartment Data from Azure Storage Data
        public ApartmentsResponse Apartment(ApartmentModel apartments)
        {
            return new ApartmentsResponse()
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
        public ApartmentModel NewApartment(ApartmentsRequest request, string imageUri)
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
        public IEnumerable<BlogsResponse> GetBlogs(IEnumerable<BlogModel> blog)
        {
            return blog.Select(GetBlog);
        }

        public BlogsResponse GetBlog(BlogModel blog)
        {

            return new BlogsResponse()
            {
                RowKey = blog.RowKey,
                PartitionKey = blog.PartitionKey,
                Message = blog.Message,
                BlogType = blog.BlogType,
                BloggerInfo = blog.BloggerInfo == null ? new BlogEntry() : JsonConvert.DeserializeObject<BlogEntry >(blog.BloggerInfo)
            };
        }

        public BlogModel NewBlog(BlogsRequest request, string imageUrl)
        {
            var id = Guid.NewGuid().ToString();

            var bloggerInfo = new BlogEntry()
            {
                Email = request.Email,
                Company = request.Company,
                ImageUri = imageUrl,
                Name = request.Name,
                Position = request.Position,
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



        //Sql

        public Apartment NewApartment(ApartmentRequest request, string imageUri)
        {
            return new Apartment()
            {
                Title = request.Title,
                Description = request.Description,
                Address = request.Address,
                Price = request.Price,
                Bedrooms = request.Bedrooms,
                Status = request.Status,
                Featured = request.Featured,
                ImageName = request.File.FileName,
                ImageUri = imageUri,
                CategoryId = request.CategoryId
            };
        }

        public IEnumerable<ApartmentResponse> GetApartments(IEnumerable<Apartment> model, IEnumerable<Category> category)
        {
            var response = from a in model
                           join c in category on a.CategoryId equals c.Id
                           select GetApartment(a, c);

            return response;
        }

        public ApartmentResponse GetApartment(Apartment model, Category category)
        {
            return new ApartmentResponse()
            {
                Uid = GuidConversion.GuidFromString(Encrytion.EncryptId(model.Id.ToString())),
                Id = model.Id,
                CategoryId = category.Id,
                CategoryName = category.CategoryName,
                Address = model.Address,
                Bedrooms=model.Bedrooms,
                Featured = model.Featured,
                ImageName = model.ImageName,
                ImageUri = model.ImageUri,
                Price = model.Price,
                Status = model.Status
            };
        }

        //Blogs
        public Blogs NewBlogs(BlogRequest request)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BlogResponse> GetBlogs(IEnumerable<Blogs> model)
        {
            throw new NotImplementedException();
        }

        public BlogResponse GetBlog(Blogs blog)
        {
            throw new NotImplementedException();
        }


        //Members
        public Member NewMemberRequest(MemberRequest model)
        {
            throw new NotImplementedException();
        }

        //Category
        public Category NewCategory(CategoryRequest request)
        {
            return new Category()
            {
                Type = request.CategoryType,
                CategoryName = request.CategoryName
            };
        }

        public Category UpdateCategory(int id, CategoryRequest request)
        {
            return new Category()
            {
                Id = id,
                CategoryName = request.CategoryName,
                Type = request.CategoryType,
            };
        }

        public IEnumerable<CategoryResponse> GetAllCategories(IEnumerable<Category> model)
        {
            return model.Select(GetCategory);
        }

        public CategoryResponse GetCategory(Category category)
        {
            return new CategoryResponse()
            {
                Uid = GuidConversion.GuidFromString(Encrytion.EncryptId(category.Id.ToString())),
                Id = category.Id,
                Type = category.Type,
                Name = category.CategoryName,
            };
        }

        //Create Request to Join membership
        public JoinMember AddNewMemberRequest(JoinRequest request)
        {
            return new JoinMember()
            {
                FirstName = request.FirstName,
                Lastname = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Citizenship = request.Citizenship,
                Comments = request.Comments,
                Country = request.Country,
                Province = request.Province,
                TownCity = request.TownCity,
                Street = request.Street
            };
        }
    }
}
