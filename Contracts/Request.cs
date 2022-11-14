using Azure;
using Microsoft.AspNetCore.Http;
using MountHebronAppApi.Models;

namespace MountHebronAppApi.Contracts
{
    public class ApartmentsRequest
    {
        public string Category { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public Double Price { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string PurchaseType { get; set; }
        public AddressModel Address { get; set; }
        public IFormFile File { get; set; }
    }

    public class BlogsRequest
    {
        public string Category { get; set; }
        public string Message { get; set; }
        public string BlogType { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public IFormFile File { get; set; }
    }

    //Sql Server contracts
    public class ApartmentRequest
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int Price { get; set; }
        public int Bedrooms { get; set; }
        public string Status { get; set; }
        public bool Featured { get; set; }
        public IFormFile File { get; set; }
    }

    public class CategoryRequest
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class BlogRequest
    {
        public int CategoryId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }
    }

    public class JoinRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Citizenship { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Comments { get; set; }
    }

    public class MemberRequest
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Citizenship { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string TownCity { get; set; }
        public string Street { get; set; }
        public IFormFile File { get; set; }
    }
}
