using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using register_auth_cokikes.Entities;

namespace register_auth_cokikes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddDbContext<DataBaseContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                //opts.UseLazyLoadingProxies();
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(opts =>
            {
                opts.Cookie.Name = "kurabieeeeeee";
                opts.ExpireTimeSpan = TimeSpan.FromDays(7);
                opts.SlidingExpiration = false;
                opts.LoginPath = "/AccountControler/Login"; 
                opts.LogoutPath = "/AccountControler/Logout";
                opts.AccessDeniedPath = "/Home/AcsessDenied";


            });
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
