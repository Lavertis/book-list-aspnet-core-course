using BookListRazor.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : Controller
{
    private readonly ApplicationDbContext _db;

    public BookController(ApplicationDbContext db)
    {
        _db = db;
    }

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
}