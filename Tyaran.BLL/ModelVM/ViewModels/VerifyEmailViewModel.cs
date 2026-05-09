using System.ComponentModel.DataAnnotations;

namespace Tyaran.BLL.ModelVM.ViewModels
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
    }
}
