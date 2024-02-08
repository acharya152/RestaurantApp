using ClassLibraryForRestro.Infrastructure;
using ClassLibraryForRestro.Services;
using ClassLibraryForRestro.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext2>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantDbContextConnection")));
builder.Services.AddScoped<IRestro, Restrorepo>();
builder.Services.AddScoped<IComments, CommentsRepo>();
builder.Services.AddScoped<IRatings, RatingRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
