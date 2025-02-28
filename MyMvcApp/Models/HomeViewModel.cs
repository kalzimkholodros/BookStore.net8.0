namespace MyMvcApp.Models;

public class HomeViewModel
{
    public IEnumerable<Book> FeaturedBooks { get; set; }
    public IEnumerable<Book> PopularBooks { get; set; }
    public IEnumerable<Book> DiscountedBooks { get; set; }
} 