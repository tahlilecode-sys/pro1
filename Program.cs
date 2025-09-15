using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using tahlilecode.Data;
using tahlilecode.Models;
using tahlilecode.Tools;

var builder = WebApplication.CreateBuilder(args);


// سایر تنظیمات
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration
	.GetConnectionString("testConnectionString")));



builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

#region Services

builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IViewRenderService, ViewRenderService>();

#endregion


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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
