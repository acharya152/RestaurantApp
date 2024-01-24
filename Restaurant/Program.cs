using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Areas.Identity.Data;
using Restaurant.Data;
using Restaurant.Infrastructure;
using Restaurant.Services;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("RestaurantDbContextConnection") ?? throw new InvalidOperationException("Connection string 'RestaurantDbContextConnection' not found.");

builder.Services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantDbContextConnection")));
builder.Services.AddDbContext<ApplicationDbContext2>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantDbContextConnection")));
builder.Services.AddDefaultIdentity<RestaurantUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<RestaurantDbContext>();
builder.Services.AddTransient<IRestro, Restrorepo>();
builder.Services.AddTransient<IComments, CommentsRepo>();
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
app.MapGet("/", ctx =>
{
    if (ctx.User.Identity.IsAuthenticated)
    {
        // User is already authenticated, redirect to another page
        ctx.Response.Redirect("/DetailsRestroes/Index");
    }
    else
    {
        // User is not authenticated, redirect to the login page
        ctx.Response.Redirect("/Identity/Account/Login");
    }

    return Task.CompletedTask;
});
app.Run();
