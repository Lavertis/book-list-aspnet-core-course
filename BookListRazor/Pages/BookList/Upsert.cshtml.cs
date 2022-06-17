using BookListRazor.Data;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList;

public class Upsert : PageModel
{
    private readonly ApplicationDbContext _db;

    public Upsert(ApplicationDbContext db)
    {
        _db = db;
    }

    [BindProperty] public Book? Book { get; set; } = default!;

    public async Task<IActionResult> OnGet(int? id)
    {
        // create
        Book = new Book();
        if (id == null)
            return Page();

        // update
        Book = await _db.Books.FindAsync(id);
        if (Book == null)
            return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
            return RedirectToPage();

        if (Book!.Id == 0)
            await _db.Books.AddAsync(Book);
        else
            _db.Books.Update(Book);

        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}