using Microsoft.EntityFrameworkCore;
using WebApplicationMedical.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

string? stringConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MedicalDbContext>(o => o.UseSqlServer(stringConnection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
