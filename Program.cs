using Microsoft.EntityFrameworkCore;
using cloudscribe.Web.SiteMap;
using Technexa.Data;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// i add sqlServerOptionsAction because i am using free database .
builder.Services.AddDbContext<DBContextApplication>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StringChainCnx"),sqlServerOptionsAction=>sqlServerOptionsAction.EnableRetryOnFailure(maxRetryCount:3,maxRetryDelay:TimeSpan.FromSeconds(2),errorNumbersToAdd:null)));
builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(30); });// Set to 30 minutes (adjust as needed)


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Technexa.Controllers.HomeController>(); // This points to your Startup.cs class
        });


app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();

