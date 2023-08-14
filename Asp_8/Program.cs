using Asp_8.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebUI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        string conn = builder.Configuration.GetConnectionString("default");
        builder.Services.AddDbContext<BookStoreDbContext>(x => x.UseSqlServer(conn));

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

        app.MapAreaControllerRoute(
            name: "user_default",
            areaName: "user",
            pattern: "{user}/{controller=BookStore}/{action=Main}/{id?}"
            );

        app.Run();
    }
}