using MongoDB.Driver;
using MyMvcApp.Models;
using MyMvcApp.Settings;

namespace MyMvcApp.Services;

public class MongoDbService
{
    private readonly IMongoCollection<Book> _booksCollection;
    private readonly IMongoCollection<User> _usersCollection;

    public MongoDbService(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _booksCollection = database.GetCollection<Book>(settings.BooksCollectionName);
        _usersCollection = database.GetCollection<User>("Users");
    }

    // Book operations
    public async Task<List<Book>> GetAsync() =>
        await _booksCollection.Find(_ => true).ToListAsync();

    public async Task<Book> GetAsync(string id) =>
        await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Book newBook) =>
        await _booksCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, Book updatedBook) =>
        await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await _booksCollection.DeleteOneAsync(x => x.Id == id);

    // User operations
    public async Task<User> GetUserByEmailAsync(string email) =>
        await _usersCollection.Find(x => x.Email == email).FirstOrDefaultAsync();

    public async Task<User> GetUserByUsernameAsync(string username) =>
        await _usersCollection.Find(x => x.Username == username).FirstOrDefaultAsync();

    public async Task CreateUserAsync(User newUser) =>
        await _usersCollection.InsertOneAsync(newUser);

    public async Task UpdateUserAsync(string id, User updatedUser) =>
        await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

    public async Task<User> GetUserByIdAsync(string id) =>
        await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    // Add these methods to the MongoDbService class
    public async Task<IEnumerable<Book>> GetFeaturedBooksAsync()
    {
        return await _booksCollection.Find(b => b.IsFeatured)
            .Limit(6)
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetPopularBooksAsync()
    {
        return await _booksCollection.Find(b => b.IsPopular)
            .Limit(10)
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetDiscountedBooksAsync()
    {
        return await _booksCollection.Find(b => b.IsDiscounted)
            .Limit(10)
            .ToListAsync();
    }
} 