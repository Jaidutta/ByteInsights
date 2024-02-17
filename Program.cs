using ByteInsights.Data;
using ByteInsights.Helpers;
using ByteInsights.Models;
using ByteInsights.Services;
using ByteInsights.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


/* builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

 Changes the code  above to use Npgsql below
*/

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

/*builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

  Replaced the above lines of code with the 2 lines below to use BlogUser
*/

builder.Services.AddIdentity<BlogUser, IdentityRole >(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultUI()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();


builder.Services.AddRazorPages();

// Register my custom DataService class
builder.Services.AddScoped<DataService>();

// Register the SearchService class
builder.Services.AddScoped<BlogSearchService>();

// Registr a pre-configured instance of the MailSettings Class
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddScoped<IBlogEmailSender, EmailService>();

// Custom ImageService Class
builder.Services.AddScoped<IImageService, BasicImageService>();


// Custom SlugService Class
builder.Services.AddScoped<ISlugService, BasicSlugService>();

var app = builder.Build();

// Resolve DataService and run initialization ManageDataAsync()
using (var scope = app.Services.CreateScope())
{
    //DataService
    var serviceProvider = scope.ServiceProvider;
    var dataService = serviceProvider.GetRequiredService<DataService>();
    await dataService.ManageDataAsync();
   
}




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "SlugRoute",
    pattern: "BlogPosts/UrlFriendly/{slug}",
    defaults: new { controller = "Posts", action = "Details" }
    );


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
