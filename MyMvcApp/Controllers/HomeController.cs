using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Services;

namespace MyMvcApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MongoDbService _mongoDbService;

    public HomeController(ILogger<HomeController> logger, MongoDbService mongoDbService)
    {
        _logger = logger;
        _mongoDbService = mongoDbService;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new HomeViewModel
        {
            FeaturedBooks = await _mongoDbService.GetFeaturedBooksAsync(),
            PopularBooks = await _mongoDbService.GetPopularBooksAsync(),
            DiscountedBooks = await _mongoDbService.GetDiscountedBooksAsync()
        };
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
