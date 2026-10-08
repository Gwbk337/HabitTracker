using HabitTracker.Data;
using HabitTracker.Models;
using HabitTracker.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


// ==========================
// DATABASE SQLITE
// ==========================

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));



// ==========================
// IDENTITY
// ==========================

builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{

    options.Password.RequireDigit = false;

    options.Password.RequiredLength = 6;

    options.Password.RequireUppercase = false;

    options.Password.RequireNonAlphanumeric = false;

})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();




// ==========================
// COOKIE SETTINGS
// ==========================

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";

    options.AccessDeniedPath = "/Account/Login";
});




// ==========================
// SERVICES
// ==========================

builder.Services.AddScoped<Exporter>();



// ==========================
// MVC
// ==========================

builder.Services.AddControllersWithViews();



var app = builder.Build();




// ==========================
// PIPELINE
// ==========================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}



app.UseHttpsRedirection();


app.UseStaticFiles();



app.UseRouting();



app.UseAuthentication();

app.UseAuthorization();




// ==========================
// ROUTES
// ==========================

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);



app.Run();