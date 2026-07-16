using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCMovie.Models;

var builder = WebApplication.CreateBuilder(args);

// Add MVC services
builder.Services.AddControllersWithViews();

// Configure database
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MvcMovieContext")
        ?? throw new InvalidOperationException("Connection string not found.")));

// Configure Identity
builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MovieContext>();

// Cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Home/AccessDenied";
});

var app = builder.Build();

// Configure middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// .NET 8.0 Static Files Handling
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Route Configuration (.NET 8.0 Compatible)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");

app.MapRazorPages();

// Create default roles and users (Development Only)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roles = { "Admin", "Viewer" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Development seed users only - remove in production!
    if (app.Environment.IsDevelopment())
    {
        await CreateUserAsync(userManager, "admin@movie.com", "Admin@123", "Admin");
        await CreateUserAsync(userManager, "viewer@movie.com", "Viewer@123", "Viewer");
    }

    SeedData.Initialize(services);
}

app.Run();

static async Task CreateUserAsync(
    UserManager<ApplicationUser> userManager,
    string email,
    string password,
    string role)
{
    var user = await userManager.FindByEmailAsync(email);

    if (user != null)
        return;

    user = new ApplicationUser
    {
        UserName = email,
        Email = email,
        EmailConfirmed = true
    };

    var result = await userManager.CreateAsync(user, password);

    if (result.Succeeded)
    {
        await userManager.AddToRoleAsync(user, role);
    }
}