using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MountHebronAppApi.Models
{
    public class Blogs
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }

        [Required]
        public string Message { get; set; }

        public int Likes { get; set; }

        public bool Featured { get; set; }

        [ForeignKey("Id")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("Id")]
        public int UserId { get; set; }
        public Member User { get; set; }
    }
}
