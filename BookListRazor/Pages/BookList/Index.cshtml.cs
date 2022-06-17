using BookListRazor.Data;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList;

public class Index : PageModel
{
    private readonly ApplicationDbContext _db;

    public Index(ApplicationDbContext db)
    {
        _db = db;
    }

    public IEnumerable<Book> Books { get; set; } = default!;

    public async Task OnGet()
    {
        Books = await _db.Books.ToListAsync();
    }

    public async Task<IActionResult> OnPostDelete(int id)
    {
        var book = await _db.Books.FindAsync(id);
        if (book == null)
            return NotFound();

        _db.Books.Remove(book);
        await _db.SaveChangesAsync();

        return RedirectToPage();
    }
}