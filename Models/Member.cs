using System.ComponentModel.DataAnnotations;

namespace MountHebronAppApi.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Lastname { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Citizenship { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string TownCity { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string ImageName { get; set; }

        [Url]
        public string ImageUri { get; set; }

        public List<Blogs> Blogs { get; set; }
    }
}
