using Microsoft.EntityFrameworkCore;
using NguyenQuocHuy.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserContext>(item
    => item.UseSqlServer(builder.Configuration.GetConnectionString("UserConnect")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
