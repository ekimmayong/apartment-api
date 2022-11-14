using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MountHebronAppApi.Models
{
    public class Apartment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Bedrooms { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public bool Featured { get; set; }

        [Required]
        public string ImageName { get; set; }

        [Url]
        public string ImageUri { get; set; }

        [ForeignKey("Id")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
