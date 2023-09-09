using App.Business.Abstract;
using App.Business.Concrete;
using Asp_8.DataBaseContext;
using BookStore.WebUI.Entities;
using BookStore.WebUI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebUI;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        string conn = builder.Configuration.GetConnectionString("default");
        builder.Services.AddDbContext<BookStoreDbContext>(x => x.UseSqlServer(conn));
        builder.Services.AddDbContext<CustomIdentityDbContext>(x => x.UseSqlServer(conn));

        builder.Services.AddIdentity<CustomIdentityUser, CustomIdentityRole>(opt => opt.Password.RequiredLength = 3)
			.AddEntityFrameworkStores<CustomIdentityDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddSession();

		//builder.Services.AddAuthorization(options =>
		//{
		//	options.AddPolicy("RequireAdminRole", policy =>
		//	{
		//		policy.RequireRole("Admin");
		//	});
		//});

		builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddSingleton<ICartSessionService, CartSessionService>();
        builder.Services.AddSingleton<ICartService, CartService>();
        builder.Services.AddSingleton<ICategoryService, CategoryService>();
        builder.Services.AddSingleton<IBooksService, BooksService>();
        builder.Services.AddSingleton<IAuthorService, AuthorService>();
        builder.Services.AddSingleton<IThemeService, ThemeService>();
        builder.Services.AddSingleton<IPressService, PressService>();
        
        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseSession();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.UseAuthentication();

        //app.MapControllerRoute(
        //    name: "userArea",
        //    pattern: "User/{controller=BookStore}/{action=Main}/{id?}",
        //    defaults: new { area = "User" }
        //    );

        //app.MapControllerRoute(
        //    name: "adminArea",
        //    pattern: "Admin/{controller=AdminBookStore}/{action=Main}/{id?}",
        //    defaults: new { area = "Admin" }
        //    );

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Account}/{action=LogIn}/{id?}"
            );

        app.Run();
    }
}