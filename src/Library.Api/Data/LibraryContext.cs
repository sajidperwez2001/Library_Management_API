using Microsoft.EntityFrameworkCore;
using Library.Api.Models;

namespace Library.Api.Data;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasIndex(b => b.ISBN).IsUnique();
        modelBuilder.Entity<User>().HasData(new User { Id = 1, Username = "admin", PasswordHash = "admin123", Role = "Admin" });
        base.OnModelCreating(modelBuilder);
    }
}
