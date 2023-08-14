using Asp_8.Entites;
using Microsoft.EntityFrameworkCore;

namespace Asp_8.Context;
public class BookStoreDbContext : DbContext
{
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Books>? Books { get; set; }
    public DbSet<Author>? Authors { get; set; }
    public DbSet<Press>? Presses { get; set; }
    public DbSet<Theme>? Themes { get; set; }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> optionsBuilder) : base(optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);

}