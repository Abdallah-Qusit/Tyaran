using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.BLL.Common;
using Tyaran.BLL.ModelVM.ViewModels;
using Tyaran.BLL.Service.Abstraction;
using Tyaran.DAL.Entities.Generated;
using Tyaran.DAL.Enum;

namespace Tyaran.BLL.Service.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TyaranDbContext _db;

        public AuthService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            TyaranDbContext db)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _db = db;
        }

        // ---------------- FILE UPLOAD ----------------
        private async Task<string> SaveFileAsync(IFormFile file, string folder)
        {
            var uploadsPath = Path.Combine("wwwroot", "uploads", folder);
            Directory.CreateDirectory(uploadsPath);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var fullPath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/uploads/{folder}/{fileName}";
        }

        // ---------------- LOGIN ----------------
        public async Task<ApiResponse<UserViewModel>> LoginAsync(LoginViewModel model)
        {
            var identityUser = await _userManager.FindByNameAsync(model.Username);
            if (identityUser == null)
                return new() { IsSuccess = false, Message = "Invalid credentials" };

            var result = await _signInManager.PasswordSignInAsync(
                model.Username, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
                return new() { IsSuccess = false, Message = "Invalid credentials" };

            var roles = await _userManager.GetRolesAsync(identityUser);

            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.IdentityUserId == identityUser.Id);

            if (user == null)
                return new() { IsSuccess = false, Message = "User profile not found" };

            if (roles.Contains("Delivery"))
            {
                var delivery = await _db.DeliveryMen
                    .FirstOrDefaultAsync(d => d.UserId == user.UserId);

                if (delivery == null)
                    return new() { IsSuccess = false, Message = "Delivery profile not found" };

                if (delivery.ApprovalStatus == (int)ApprovalStatusEnum.Pending)
                {
                    await _signInManager.SignOutAsync();
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Your delivery account is pending admin approval."
                    };
                }

                if (delivery.ApprovalStatus == (int)ApprovalStatusEnum.Rejected)
                {
                    await _signInManager.SignOutAsync();
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Your delivery account was rejected by admin."
                    };
                }
            }
            if (roles.Contains("RestaurantOwner"))
            {
                var restaurant = await _db.Restaurants
                    .FirstOrDefaultAsync(r => r.OwnerId == user.UserId);

                if (restaurant == null)
                    return new() { IsSuccess = false, Message = "Restaurant profile not found" };

                if (restaurant.ApprovalStatus == (int)ApprovalStatusEnum.Pending)
                {
                    await _signInManager.SignOutAsync();
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Your restaurant account is pending admin approval."
                    };
                }

                if (restaurant.ApprovalStatus == (int)ApprovalStatusEnum.Rejected)
                {
                    await _signInManager.SignOutAsync();
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Your restaurant account was rejected by admin."
                    };
                }
            }
            return new ApiResponse<UserViewModel>
            {
                IsSuccess = true,
                Message = "Login successful",
                Data = new UserViewModel
                {
                    UserId = user.UserId,
                    Email = identityUser.Email!,
                    FullName = identityUser.FullName!,
                    Role = roles.FirstOrDefault() ?? "User"
                }
            };
        }

        // ---------------- CHANGE PASSWORD ----------------
        public async Task<ApiResponse> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            var user = await _userManager.GetUserAsync(_signInManager.Context.User);

            if (user == null)
                return ApiResponse.Fail("User is not logged in.");

            var result = await _userManager.ChangePasswordAsync(
                user,
                model.CurrentPassword,
                model.NewPassword
            );

            if (!result.Succeeded)
            {
                return ApiResponse.Fail(
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            await _signInManager.RefreshSignInAsync(user);

            return ApiResponse.Success("Password changed successfully.");
        }

        // ---------------- REGISTER USER ----------------
        public async Task<ApiResponse> RegisterUserAsync(RegisterUserViewModel model)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var identityUser = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,          // FIX: use actual email field
                    PhoneNumber = model.Phone,
                    FullName = model.Name
                };

                var result = await _userManager.CreateAsync(identityUser, model.Password);
                if (!result.Succeeded)
                    return ApiResponse.Fail(string.Join(", ", result.Errors.Select(e => e.Description)));

                await _userManager.AddToRoleAsync(identityUser, "User");

                var user = new User
                {
                    IdentityUserId = identityUser.Id,
                    Email = model.Email,
                    FirstName = model.Name,
                    CreatedAt = DateTime.UtcNow,
                    InActive = false
                };

                _db.Users.Add(user);
                await _db.SaveChangesAsync(); 

                _db.UserPhones.Add(new UserPhone
                {
                    UserId = user.UserId,     
                    PhoneNumber = model.Phone,
                });

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                return ApiResponse.Success("User registered successfully");
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // ---------------- REGISTER DELIVERY ----------------
        public async Task<ApiResponse> RegisterDeliveryAsync(RegisterDeliveryViewModel model)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var identityUser = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,          // FIX: use a real email, not Username
                    PhoneNumber = model.Phone,
                    FullName = model.Name
                };

                var result = await _userManager.CreateAsync(identityUser, model.Password);
                if (!result.Succeeded)
                    return ApiResponse.Fail(string.Join(", ", result.Errors.Select(e => e.Description)));

                await _userManager.AddToRoleAsync(identityUser, "Delivery");

                var nationalIdUrl = await SaveFileAsync(model.NationalIdFile, "delivery-ids");

                var user = new User
                {
                    IdentityUserId = identityUser.Id,
                    Email = model.Email,        
                    FirstName = model.Name,       
                    CreatedAt = DateTime.UtcNow,
                    InActive = false
                };

                _db.Users.Add(user);
                await _db.SaveChangesAsync();

                _db.UserPhones.Add(new UserPhone
                {
                    UserId = user.UserId,
                    PhoneNumber = model.Phone,
                });

                await _db.SaveChangesAsync();

                _db.DeliveryMen.Add(new DeliveryMan
                {
                    UserId = user.UserId,
                    VehicleType = model.VehicleType,
                    CreatedAt = DateTime.UtcNow,
                    IsAvailable = true,
                    IsOnline = false,
                    NationalIdPath = nationalIdUrl,
                    ApprovalStatus = (int)ApprovalStatusEnum.Pending,
                });

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                return ApiResponse.Success("Delivery registered successfully");
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // ---------------- REGISTER RESTAURANT ----------------
        public async Task<ApiResponse> RegisterRestaurantAsync(RegisterRestaurantViewModel model)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {

                if (model.Latitude == 0 || model.Longitude == 0)
                {
                    return ApiResponse.Fail("Please select the restaurant location on the map.");
                }

                var identityUser = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,          // FIX: use a real email, not Username
                    PhoneNumber = model.Phone,
                    FullName = model.Name
                };

                var result = await _userManager.CreateAsync(identityUser, model.Password);
                if (!result.Succeeded)
                    return ApiResponse.Fail(string.Join(", ", result.Errors.Select(e => e.Description)));

                await _userManager.AddToRoleAsync(identityUser, "RestaurantOwner");

                var logoUrl = await SaveFileAsync(model.Logo, "restaurant-logos");
                var registerUrl = await SaveFileAsync(model.CommercialRegister, "restaurant-registers");

                var user = new User
                {
                    IdentityUserId = identityUser.Id,
                    Email = model.Email,          
                    FirstName = model.Name,       
                    CreatedAt = DateTime.UtcNow,
                    InActive = false
                };

                _db.Users.Add(user);
                await _db.SaveChangesAsync();

                var address = new Address
                {
                    Street = model.Street,
                    City = model.City,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    CreatedAt = DateTime.UtcNow
                };

                _db.Addresses.Add(address);
                await _db.SaveChangesAsync();

                var restaurant = new Restaurant
                {
                    OwnerId = user.UserId,
                    Name = model.RestaurantName,
                    AddressId = address.AddressId,
                    LogoUrl = logoUrl,
                    CommercialRegisterPath = registerUrl,
                    CreatedAt = DateTime.UtcNow,
                    ApprovalStatus = (int)ApprovalStatusEnum.Pending,
                    IsActive = true
                };

                _db.Restaurants.Add(restaurant);
                await _db.SaveChangesAsync(); 

                _db.RestaurantPhones.Add(new RestaurantPhone
                {
                    RestaurantId = restaurant.RestaurantId,
                    PhoneNumber = model.Phone
                });

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                return ApiResponse.Success("Restaurant owner registered successfully");
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // ---------------- LOGOUT ----------------
        public async Task<ApiResponse> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return ApiResponse.Success("Logged out successfully");
        }

    }
}
