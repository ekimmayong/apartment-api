using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MountHebronAppApi.Models
{
    public class JoinMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Lastname { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Citizenship { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string TownCity { get; set; }

        [Required]
        public string Street { get; set; }

        public string Comments { get; set; }
    }
}
