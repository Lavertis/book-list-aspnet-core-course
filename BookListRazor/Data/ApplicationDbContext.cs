using BookListRazor.Models;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; } = default!;
}