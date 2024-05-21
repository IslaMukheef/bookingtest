using backend;
using backend.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddAuthentication(x =>
{
	x.RequireAuthenticatedSignIn = false;
	x.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
				.AddCookie(x =>
				{
					x.LogoutPath = "/account/signout";
				});
var app = builder.Build();
app.UseAuthentication();
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{

	var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>()!;
	context.Database.EnsureCreated();

}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
Directory.CreateDirectory("assets");

app.UseStaticFiles(new StaticFileOptions()
{

	RequestPath = "/assets",
	FileProvider = new PhysicalFileProvider(
		   Path.Combine(builder.Environment.ContentRootPath, "assets")),
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
