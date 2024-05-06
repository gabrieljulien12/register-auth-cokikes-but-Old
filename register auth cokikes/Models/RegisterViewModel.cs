
using System.ComponentModel.DataAnnotations;

namespace register_auth_cokikes.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "username is required")]
        [StringLength(30, ErrorMessage = "max character 30.")]
        public string Username { get; set; }


        [Required(ErrorMessage = "password is requared")]
        [MinLength(6, ErrorMessage = "min character 6")]
        [MaxLength(16, ErrorMessage = "max character 16")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Re-password is requared")]
        [MinLength(6, ErrorMessage = "min character 6")]
        [MaxLength(16, ErrorMessage = "max character 16")]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }
    }


}
