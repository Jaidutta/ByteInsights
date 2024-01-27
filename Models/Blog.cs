using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ByteInsights.Models
{
    public class Blog
    {
        public int Id { get; set; }  
        public string AuthorId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 2)]
        // 0  --> property , 1 --> maximum/1st arg, 2 --> minimum/3rd argument
        public string Name { get; set; } // category


        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 2)]
        // 0  --> property , 1 --> maximum/1st arg, 2 --> minimum/3rd argument
        public string Description { get; set; }


        
        [DataType(DataType.Date)] 
        /* When displaying on a form, it will display only the Date and not date time//
         * it won't prompt me for the date and time and will only treat it as date
        */
        [Display(Name ="Created Date")] // Will display the field on the form as "Update Date"
        public DateTime Created { get; set; }


        [DataType(DataType.Date)]
        /* When displaying on a form, it will display only the Date and not date time//
         * it won't prompt me for the date and time and will only treat it as date
        */
        [Display(Name = "Updated Date")]  // Will display the field on the form as "Update Date"
        public DateTime? Updated { get; set;} // can be nullable

        [Display(Name = "Blog Image")] // Will display the field on the form as "Blog Image"
        public byte[] ImageData { get; set; } // bytestream of a physical file


        [Display(Name = "Image Type")]   // Will display the field on the form as "Image Type"
        public string ContentType { get; set; } // the type of imge jpg, png, etc


        [NotMapped]   // Not stored as a column in the db
        public IFormFile Image { get; set; }
    }
}
