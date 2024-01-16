using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("RestaurantDbContextConnection") ?? throw new InvalidOperationException("Connection string 'RestaurantDbContextConnection' not found.");

builder.Services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<RestaurantUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<RestaurantDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();//to run identity pages
app.MapGet("/", ctx =>//redirect to login page directly
{

    ctx.Response.Redirect("/Identity/Account/Login");
    return Task.CompletedTask;
});
app.Run();
