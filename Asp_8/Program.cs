using App.Business.Abstract;
using App.Business.Concrete;
using Asp_8.DataBaseContext;
using BookStore.WebUI.Services;
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
        builder.Services.AddSession();

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

        app.MapControllerRoute(
            name: "default",
            pattern: "{area:exists?}/{controller=BookStore}/{action=Main}/{id?}"
            );

        app.MapAreaControllerRoute(
            name: "admin_default",
            areaName: "admin",
            pattern: "{admin}/{controller=AdminBookStore}/{action=Main}/{id?}"
            );

        app.Run();
    }
}