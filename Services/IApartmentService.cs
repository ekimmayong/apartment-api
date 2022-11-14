using MountHebronAppApi.Contracts;
using MountHebronAppApi.Models;

namespace MountHebronAppApi.Services
{
    public interface IApartmentService
    {
        //Apartments
        Task<Apartment> AddNewApartment(ApartmentRequest request);

        Task<IEnumerable<ApartmentResponse>> GetApartments();

        Task<ApartmentResponse> GetApartment();

        Task DeleteApartment(Guid uid);

        //Blog
        Task<BlogRequest> AddNewBlog(BlogRequest model);

        Task<IEnumerable<BlogResponse>> GetBlogs();

        Task<BlogResponse> GetBlog();

        Task DeleteBlog(Guid uid);

        //Categories
        Task<CategoryRequest> AddNewCategory(CategoryRequest request);

        Task<IEnumerable<CategoryResponse>> GetCategories();

        Task<CategoryResponse> GetCategory();

        //Users
        Task<MemberRequest> NewMembers(MemberRequest model);

        Task<IEnumerable<MemberResponse>> GetMembers();

        Task<MemberResponse> GetMember();

        //Join Request
        Task<JoinRequest> AddNewJoin(JoinRequest model);

    }
}
