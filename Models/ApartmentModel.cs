
using Azure;
using Azure.Data.Tables;

namespace MountHebronAppApi.Models
{
    public class ApartmentModel: ITableEntity
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public Double Price { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string ImageName { get; set; }
        public string ImageUri { get; set; }
        public string Status { get; set; }
        public string PurchaseType { get; set; }
        //public List<Comments> Comments { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }

    public class Comments
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Messages { get; set; }
    }
}