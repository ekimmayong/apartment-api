using Azure;
using Azure.Data.Tables;

namespace MountHebronAppApi.Models
{
    public class BlogModel:ITableEntity
    {
        public string BlogType { get; set; }
        public string BloggerInfo { get; set; }
        public string Message { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }

    public class BlogEntry
    {
        public string Email { get; set; } = "";
        public string Name { get; set; } = "";
        public string ImageUri { get; set; } = "";
        public string ImageName { get; set; }
        public string Position { get; set; } = "";
        public string Company { get; set; } = "";
    }
}
