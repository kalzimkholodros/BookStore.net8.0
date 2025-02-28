using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models;

public class Book
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [Required]
    [StringLength(100)]
    [BsonElement("title")]
    public string Title { get; set; }

    [Required]
    [StringLength(100)]
    [BsonElement("author")]
    public string Author { get; set; }

    [Required]
    [BsonRepresentation(BsonType.Decimal128)]
    [BsonElement("price")]
    public decimal Price { get; set; }

    [StringLength(500)]
    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("imageUrl")]
    public string ImageUrl { get; set; }

    [BsonElement("stockQuantity")]
    public int StockQuantity { get; set; }

    [StringLength(50)]
    [BsonElement("isbn")]
    public string ISBN { get; set; }

    [Required]
    [StringLength(50)]
    [BsonElement("category")]
    public string Category { get; set; }

    [BsonElement("isFeatured")]
    public bool IsFeatured { get; set; }

    [BsonElement("isPopular")]
    public bool IsPopular { get; set; }

    [BsonElement("isDiscounted")]
    public bool IsDiscounted { get; set; }

    [BsonElement("discountedPrice")]
    public decimal? DiscountedPrice { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    public DateTime? UpdatedAt { get; set; }
} 