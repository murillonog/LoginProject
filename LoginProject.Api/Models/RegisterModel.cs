using LoginProject.Application.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LoginProject.Api.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords don´t match")]
        public string ConfirmPassword { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public DateTime Birth { get; set; }
        [Required, MaxLength(255)]
        public string Address { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public int Age { get; private set; }
    }
}
