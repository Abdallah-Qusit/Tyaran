using System.ComponentModel.DataAnnotations;

namespace Tyaran.BLL.ModelVM.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }


        public string? Street { get; set; }

        public string? City { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude
        {
            get; set;
        }


        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password does not match.")]
        public string ConfirmPassword { get; set; }
    }
}
