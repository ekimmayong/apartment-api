using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MountHebronAppApi.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public List<Apartment> Apartments { get; set; }
        public List<Blogs> Blogs { get; set; }
    }
}
