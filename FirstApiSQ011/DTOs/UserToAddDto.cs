using System.ComponentModel.DataAnnotations;

namespace FirstApiSQ011.DTOs
{
    public class UserToAddDto
    {
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "3 to 15 characters allowed!")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "3 to 15 characters allowed!")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Mismatched!")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [EmailAddress]
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "3 to 15 characters allowed!")]
        public string Email { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "3 to 15 characters allowed!")]
        public string Role { get; set; }
    }
}
