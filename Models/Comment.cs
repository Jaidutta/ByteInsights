using ByteInsights.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ByteInsights.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }

        public string? BlogUserId { get; set; }

        public string? ModeratorId {  get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 2)]
        [Display(Name = "Comment")]
        public string? Body { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime? Created { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Updated Date")]
        public DateTime? Updated { get; set; }

        public DateTime? Moderated { get; set; }

        public DateTime? Deleted { get; set; }

        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 2)]
        [Display(Name = "Moderated Comment")]
        public string? ModeratedBody { get; set; }

        public ModerationType? ModerationType { get; set; }

        // Navigation properties

        public virtual Post? Post { get; set; }

        public virtual BlogUser? BlogUser { get; set; }

        public virtual BlogUser? Moderator { get; set; }

        /* We are more used to seeing public virtual BlogUser BlogUser { get; set; }
         * but public virtual BlogUser Moderator { get; set; } is also acceptable
         * BlogUser is the type and Moderator is the navigation property
         * The name Moderator should match the ModeratorId
         * public string ModeratorId {  get; set; }
         * 
         * As long as public string ModeratorId {  get; set; }
         * matches public virtual BlogUser Moderator { get; set; }
         * we are good to go. That is Moderator and the property name ModeratorId
         */


    }
}
