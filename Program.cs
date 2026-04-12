using BabakBlog;
using BabakBlog.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

//--------------------------------------------------------------------------------
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(SharedResource));
    });

builder.Services.Configure<RequestLocalizationOptions>(options => {
    var supportedCultures = new[] { "tr", "en", "ru" }; 
    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
        /*options.RequestCultureProviders.Clear(); options.RequestCultureProviders.Add(new CookieRequestCultureProvider());*/ 
    options.RequestCultureProviders = new List<IRequestCultureProvider> 
    { new CookieRequestCultureProvider(), 
        new QueryStringRequestCultureProvider() 
    }; 
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("tr"),
        new CultureInfo("en"),
        new CultureInfo("ru")
    };

    options.DefaultRequestCulture = new RequestCulture("tr");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider()
    };
});
//-----------------------------------------------------------------------------

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 1;
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Auth/AccessDenied";
    options.LoginPath = "/Auth/Login";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;

});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
   var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
   var _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string adminEmail = "adminbabak@gmail.com";
    string adminPassword = "((1234))";

    var axistingAdminRole = await _roleManager.FindByNameAsync("Admin");

    if(axistingAdminRole == null)
    {
        await _roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    var existingAdminUser = await _userManager.FindByEmailAsync(adminEmail);

    if (existingAdminUser == null)
    {
        await _userManager.CreateAsync(new IdentityUser { UserName = adminEmail, Email = adminEmail }, adminPassword);
        await _userManager.AddToRoleAsync(new IdentityUser { UserName = adminEmail, Email = adminEmail }, "Admin");
    }
}

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

/*app.UseRequestLocalization();*/
app.UseRequestLocalization(
    app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value
);

app.UseHttpsRedirection();
app.UseStaticFiles();



app.UseRouting();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Post}/{action=Index}/{id?}");

app.Run();
