using Focus.Common;
using Focus.Common.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using Required = Focus.Common.Attributes.RequiredAttribute;

namespace NoRoom.Common
{
    [Module("profile", "Profile")]
    [Title("Guest Profile")]
    public class Guest : BaseModel
    {
        [Display(Name = "First Name")]
        [SearchProperty]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Passport Number")]
        [Required]
        public string PassportNumber { get; set; }

        [Display(Name = "Date of Birth")]
        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
