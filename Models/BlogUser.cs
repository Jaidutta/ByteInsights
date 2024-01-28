﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ByteInsights.Models
{
    public class BlogUser : IdentityUser
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 2)]
        public string LastName { get; set; }

        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 2)]
        public string? LinkedinUrl { get; set; }


        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 2)]
        public string? GithubUrl { get; set; }


        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 2)]
        public string? TwitterUrl { get; set; }


        [NotMapped]
        public string FullName { 
            get
            { 
                return $"{FirstName} {LastName}";
            }
                 
        }

        // Virtual properties

        public virtual ICollection<Blog> Blogs { get; set; } = new HashSet<Blog>();

        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}