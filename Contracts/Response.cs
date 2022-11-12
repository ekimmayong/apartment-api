using MountHebronAppApi.Models;

namespace MountHebronAppApi.Contracts
{
    public class ApartmentResponse
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

    public class BlogResponse
    {
        public string RowKey { get; set; }
        public string PartitionKey { get; set; }
        public string Message { get; set; }
        public string BlogType { get; set; }
        public BlogEntry BloggerInfo { get; set; }
    }
}
