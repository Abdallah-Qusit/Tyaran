using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Tyaran.BLL.ModelVM.ViewModels
{
    public class RegisterDeliveryViewModel : RegisterViewModel
    {
        [Required]
        public string VehicleType { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile NationalIdFile { get; set; }

        // FIX: Delivery/Restaurant need a real email (Identity RequireUniqueEmail=true)
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}