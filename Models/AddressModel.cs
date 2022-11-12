namespace MountHebronAppApi.Models
{
    public class AddressModel
    {
        public string Country { get; set; } = "Philippines";
        public string Province { get; set; } = "";
        public string Town { get; set; } = "";
        public string City { get; set; } = "";
        public int ZipCode { get; set; }
    }
}
