using EMY.Papel.Restaurant.Infrastructure.Persistence;
using EMY.Papel.Restaurant.Web.Statics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddPersistanceServices();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);

    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Login";
    options.SlidingExpiration = true;
});

builder.Services.AddAuthentication(SystemMainStatics.DefaultScheme)
             .AddCookie(SystemMainStatics.DefaultScheme, options =>
             {
                 options.LoginPath = new PathString("/Account/Login");
                 options.AccessDeniedPath = "/Account/Login";
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

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
