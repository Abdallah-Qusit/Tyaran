using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tyaran.DAL.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


// ------------------- Database (Business) -------------------
var connectionString = builder.Configuration.GetConnectionString("Tyaran");

builder.Services.AddDbContext<TyaranDbContext>(options =>
    options.UseSqlServer(connectionString));

// ------------------- Database (Identity) -------------------
builder.Services.AddDbContext<IdentityAppDbContext>(options =>
    options.UseSqlServer(connectionString));

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
