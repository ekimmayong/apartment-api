using MountHebronAppApi.Models;
using MountHebronAppApi.Contracts;

namespace MountHebronAppApi.Mapper
{
    public interface IApartmentMapper
    {
        // Apartmetn Mappers
        ApartmentModel NewApartment(ApartmentsRequest request, string imageUri);

        IEnumerable<ApartmentsResponse> Apartments(IEnumerable<ApartmentModel> apartments);

        ApartmentsResponse Apartment(ApartmentModel apartments);

        //Blog Mappers
        IEnumerable<BlogsResponse> GetBlogs(IEnumerable<BlogModel> blog);

        BlogsResponse GetBlog(BlogModel blog);

        BlogModel NewBlog(BlogsRequest request, string imageUrl);


        ///Apartment
        Apartment NewApartment(ApartmentRequest request, string imageUri);

        IEnumerable<ApartmentResponse> GetApartments(IEnumerable<Apartment> model, IEnumerable<Category> category);

        ApartmentResponse GetApartment(Apartment model, Category category);


        //Blogs

        Blogs NewBlogs(BlogRequest request);

        IEnumerable<BlogResponse> GetBlogs(IEnumerable<Blogs> model);

        BlogResponse GetBlog(Blogs blog);


        //Members
        //Add members from the request
        Member NewMemberRequest(MemberRequest model);


        //Category
        Category NewCategory(CategoryRequest request);

        //Create Request to Join membership
        JoinMember AddNewMemberRequest(JoinRequest request);
    }
}
