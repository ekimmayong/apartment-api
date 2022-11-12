using MountHebronAppApi.Models;
using MountHebronAppApi.Contracts;

namespace MountHebronAppApi.Mapper
{
    public interface IApartmentMapper
    {
        // Apartmetn Mappers
        ApartmentModel NewApartment(ApartmentRequest request, string imageUri);

        IEnumerable<ApartmentResponse> Apartments(IEnumerable<ApartmentModel> apartments);

        ApartmentResponse Apartment(ApartmentModel apartments);

        //Blog Mappers
        IEnumerable<BlogResponse> GetBlogs(IEnumerable<BlogModel> blog);

        BlogResponse GetBlog(BlogModel blog);

        BlogModel NewBlog(BlogRequest request, string imageUrl);

    }
}
