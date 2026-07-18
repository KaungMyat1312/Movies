using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCMovie.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database Connection
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcMovieContext")));

// Identity Setup
builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MovieContext>();

// Password Configuration
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.SignIn.RequireConfirmedAccount = false;
});

// Cookie Configuration for Login Path
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Home/AccessDenied";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
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

// Default Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");

app.MapRazorPages();

// Seed Roles and Users
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    // Create Admin Role if not exists
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Create Viewer Role if not exists
    if (!await roleManager.RoleExistsAsync("Viewer"))
    {
        await roleManager.CreateAsync(new IdentityRole("Viewer"));
    }

    // Create Default Admin User
    var admin1 = await userManager.FindByEmailAsync("admin@movie.com");
    if (admin1 == null)
    {
        var newAdmin1 = new ApplicationUser { UserName = "admin@movie.com", Email = "admin@movie.com", EmailConfirmed = true };
        var result = await userManager.CreateAsync(newAdmin1, "Admin@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin1, "Admin");
        }
    }

    // Create Second Admin User
    var admin2 = await userManager.FindByEmailAsync("newadmin@movie.com");
    if (admin2 == null)
    {
        var newAdmin2 = new ApplicationUser { UserName = "newadmin@movie.com", Email = "newadmin@movie.com", EmailConfirmed = true };
        var result = await userManager.CreateAsync(newAdmin2, "Admin@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin2, "Admin");
        }
    }

    // Create Viewer User
    var viewer = await userManager.FindByEmailAsync("viewer@movie.com");
    if (viewer == null)
    {
        var newViewer = new ApplicationUser { UserName = "viewer@movie.com", Email = "viewer@movie.com", EmailConfirmed = true };
        var result = await userManager.CreateAsync(newViewer, "Viewer@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newViewer, "Viewer");
        }
    }

    // Run original seed data
    SeedData.Initialize(services);
}

app.Run();  