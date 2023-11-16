using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<Context>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("MariaDbConnectionString"),
        new MySqlServerVersion(new Version(10,3,0)));
});
builder.Services.AddSingleton<UrlShorter>();
builder.Services.AddScoped<DataValidator>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
