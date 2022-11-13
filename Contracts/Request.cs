using Azure;
using Microsoft.AspNetCore.Http;
using MountHebronAppApi.Models;

namespace MountHebronAppApi.Contracts
{
    public class ApartmentRequest
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

    public class BlogRequest
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
}
