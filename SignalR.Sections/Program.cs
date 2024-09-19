using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SignalR.Sections;
using SignalR.Sections.Data;
using SignalR.Sections.Hubs;
using System;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;
var configuration = builder.Configuration;

SD.CurrentUrl = configuration["CurrentUrl"] ?? "";

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
    .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var app = builder.Build();

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

app.UseAuthorization();

app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// SignalR
app.MapHub<UserHub>("/hubs/userCount");
app.MapHub<DeathlyHallowsHub>("/hubs/deathlyHallows");
app.MapHub<HouseGroupHub>("/hubs/houseGroup");
app.MapHub<NotificationProjectHub>("/hubs/notificationProject");
app.MapHub<BasicChatHub>("/hubs/basicChat");
app.MapHub<OrderUpdateHub>("/hubs/orderUpdateHub");

app.Run();
