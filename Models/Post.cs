using System.ComponentModel.DataAnnotations;

namespace ByteInsights.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string AuthorId { get; set; }


        [Required]
        [StringLength(75, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 2)]
        public string Abstract { get; set; }

        [Required]
        public string Content { get; set; }


        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
