using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.BLL.Common;
using Tyaran.BLL.ModelVM.ViewModels;

namespace Tyaran.BLL.Service.Abstraction
{
    public interface IAuthService
    {
        Task<ApiResponse<UserViewModel>> LoginAsync(LoginViewModel model);

        Task<ApiResponse> RegisterUserAsync(RegisterUserViewModel model);
        Task<ApiResponse> RegisterDeliveryAsync(RegisterDeliveryViewModel model);
        Task<ApiResponse> RegisterRestaurantAsync(RegisterRestaurantViewModel model);

        Task<ApiResponse> LogoutAsync();
        Task<ApiResponse> ChangePasswordAsync(ChangePasswordViewModel model);
    }

}
