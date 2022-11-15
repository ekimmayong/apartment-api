using MountHebronAppApi.Models;

namespace MountHebronAppApi.Contracts
{
    public class ApartmentsResponse
    {
        public string RowKey { get; set; }
        public string PartitionKey { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public Double Price { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public string ImageUri { get; set; }
        public string Status { get; set; }
        public string PurchaseType { get; set; }
        public AddressModel Address { get; set; }
    }

    public class BlogsResponse
    {
        public string RowKey { get; set; }
        public string PartitionKey { get; set; }
        public string Message { get; set; }
        public string BlogType { get; set; }
        public BlogEntry BloggerInfo { get; set; }
    }


    //SQL
    public class ApartmentResponse
    {
        public Guid Uid { get; set; }
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Address { get; set; }
        public int Price { get; set; }
        public int Bedrooms { get; set; }
        public string ImageName { get; set; }
        public string ImageUri { get; set; }
        public string Status { get; set; }
        public bool Featured { get; set; }
    }

    public class BlogResponse
    {
        public Guid Uid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
    }

    public class CategoryResponse
    {
        public Guid Uid { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public class JoinResponse
    {
        public Guid Uid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Citizenship { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Comments { get; set; }
    }

    public class MemberResponse
    {
        public Guid Uid { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Citizenship { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string TownCity { get; set; }
        public string Street { get; set; }
        public string ImageName { get; set; }
        public string ImageUri { get; set; }
    }
}
