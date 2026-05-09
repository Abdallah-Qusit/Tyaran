using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Tyaran.BLL.ModelVM.ViewModels
{
    public class RegisterRestaurantViewModel : RegisterViewModel
    {
        [Required]
        public string RestaurantName { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile Logo { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile CommercialRegister { get; set; }

        // FIX: need a real unique email for Identity
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}