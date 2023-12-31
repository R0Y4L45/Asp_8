﻿using Asp_8.Entites;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Asp_8.DataBaseContext;
public class BookStoreDbContext : DbContext
{
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Books>? Books { get; set; }
    public DbSet<Author>? Authors { get; set; }
    public DbSet<Press>? Presses { get; set; }
    public DbSet<Theme>? Themes { get; set; }

    public BookStoreDbContext()
    {
    }
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> optionsBuilder) : base(optionsBuilder)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder();
            string conn = builder.Configuration.GetConnectionString("default");
            optionsBuilder.UseSqlServer(conn);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);
}