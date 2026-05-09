using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tyaran.BLL.ModelVM.ViewModels
{
    public class RegisterUserViewModel : RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
