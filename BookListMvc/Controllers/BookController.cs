using BookListMvc.Data;
using BookListMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListMvc.Controllers;

public class BookController : Controller
{
    private readonly ApplicationDbContext _db;

    public BookController(ApplicationDbContext db)
    {
        _db = db;
    }

    [BindProperty] public Book? Book { get; set; }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Upsert(int? id)
    {
        Book = new Book();

        // create
        if (id == null) return View(Book);

        // update
        Book = await _db.Books.FindAsync(id);
        if (Book == null)
            return NotFound();

        return View(Book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert()
    {
        if (!ModelState.IsValid)
            return View(Book);

        if (Book.Id == 0)
            _db.Books.Add(Book);
        else
            _db.Books.Update(Book);

        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    #region API Calls

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Json(new {data = await _db.Books.ToListAsync()});
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var bookFromDb = await _db.Books.FindAsync(id);
        if (bookFromDb == null)
            return Json(new {success = false, message = "Error while deleting"});

        _db.Books.Remove(bookFromDb);
        await _db.SaveChangesAsync();
        return Json(new {success = true, message = "Delete successful"});
    }

    #endregion
}