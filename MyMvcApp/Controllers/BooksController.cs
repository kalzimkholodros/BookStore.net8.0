using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Services;

namespace MyMvcApp.Controllers;

public class BooksController : Controller
{
    private readonly MongoDbService _mongoDbService;

    public BooksController(MongoDbService mongoDbService)
    {
        _mongoDbService = mongoDbService;
    }

    // GET: Books
    public async Task<IActionResult> Index()
    {
        var books = await _mongoDbService.GetAsync();
        return View(books);
    }

    // GET: Books/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _mongoDbService.GetAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    // GET: Books/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Books/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Author,Price,Description,ImageUrl,StockQuantity,ISBN,Category,IsFeatured,IsPopular,IsDiscounted,DiscountedPrice")] Book book)
    {
        if (ModelState.IsValid)
        {
            book.CreatedAt = DateTime.UtcNow;
            await _mongoDbService.CreateAsync(book);
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }

    // GET: Books/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _mongoDbService.GetAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    // POST: Books/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Author,Price,Description,ImageUrl,StockQuantity,ISBN,Category,IsFeatured,IsPopular,IsDiscounted,DiscountedPrice")] Book book)
    {
        if (id != book.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            book.UpdatedAt = DateTime.UtcNow;
            await _mongoDbService.UpdateAsync(id, book);
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }

    // GET: Books/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _mongoDbService.GetAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    // POST: Books/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        await _mongoDbService.RemoveAsync(id);
        return RedirectToAction(nameof(Index));
    }
} 