using BookListRazor.Data;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList;

public class Create : PageModel
{
    private readonly ApplicationDbContext _db;

    public Create(ApplicationDbContext db)
    {
        _db = db;
    }

    [BindProperty] public Book Book { get; set; } = default!;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        await _db.Books.AddAsync(Book);
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}