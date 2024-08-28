
using Ipet.Configurations;
using Ipet.Data.Context;
using Ipet.MVC.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ipet.MVC.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

// ConfigureServices
builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.AddDbContext<MeuDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("connection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMvcConfiguration();
builder.Services.ResolveDependencies();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/erro/500");
    app.UseStatusCodePagesWithRedirects("/erro/{0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseGlobalizationConfig();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "contato",
    pattern: "contato",
    defaults: new { controller = "Home", action = "Contato" });

app.MapControllerRoute(
    name: "compra",
    pattern: "carrinho",
    defaults: new { controller = "Carrinho", action = "Compra" });

app.MapRazorPages();

app.Run();
