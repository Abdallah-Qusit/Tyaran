using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tyaran.BLL.Service.Abstraction;
using Tyaran.BLL.Service.Implementation;
using Tyaran.DAL.Database;
using Tyaran.DAL.Repo.Abstraction;
using Tyaran.DAL.Repo.Implementation;
//using Tyaran.DAL.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


// ------------------- Database (Business) -------------------
var connectionString = builder.Configuration.GetConnectionString("Tyaran");

builder.Services.AddDbContext<TyaranDbContext>(options =>
    options.UseSqlServer(connectionString));

// ------------------- Database (Identity) -------------------
builder.Services.AddDbContext<IdentityAppDbContext>(options =>
    options.UseSqlServer(connectionString));
//Repos
builder.Services.AddScoped<IUserHomeRepo,UserHomeRepo>();
builder.Services.AddScoped<IUserHomeService,UserHomeService>();
builder.Services.AddScoped<IUserCartRepo,UserCartRepo>();
builder.Services.AddScoped<IUserCartService,UserCartService>();
builder.Services.AddScoped<IUserMenuRepo,UserMenuRepo>();
builder.Services.AddScoped<IUserMenuService,UserMenuService>();
builder.Services.AddScoped<IUserProfileRepo,UserProfileRepo>();
builder.Services.AddScoped<IUserProfileService,UserProfileService>();
builder.Services.AddScoped<IUserTrackRepo,UserTrackRepo>();
builder.Services.AddScoped<IUserTrackService,UserTrackService>();
builder.Services.AddScoped<IDeliveryStatusRepo,DeliveryStatusRepo>();
builder.Services.AddScoped<IDeliveryStatusService,DeliveryStatusService>();
// ------------------- Identity -------------------
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;

    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<IdentityAppDbContext>()
.AddDefaultTokenProviders();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
