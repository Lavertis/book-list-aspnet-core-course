using BookListMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace BookListMvc.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; } = default!;
}