using BookListRazor.Data;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList;

public class Edit : PageModel
{
    private readonly ApplicationDbContext _db;

    public Edit(ApplicationDbContext db)
    {
        _db = db;
    }

    [BindProperty] public Book Book { get; set; } = default!;

    public async Task OnGet(int id)
    {
        Book = (await _db.Books.FindAsync(id))!;
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
            return RedirectToPage();

        var bookFromDb = (await _db.Books.FindAsync(Book.Id))!;
        bookFromDb.Name = Book.Name;
        bookFromDb.ISBN = Book.Name;
        bookFromDb.Author = Book.Author;

        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}