namespace MinimalApi;

//public record Product(int ProductId, string Name, string Description, double Price);

// Fix the Product class - remove the primary constructor or fix it properly
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }

    // Parameterless constructor for JSON deserialization
    public Product() { }

    // Constructor with parameters
    public Product(int productId, string name, string description, double price)
    {
        ProductId = productId;
        Name = name;
        Description = description;
        Price = price;
    }
}
public class ProductSeed
{

    public static List<Product> clothingProducts = new()
    {
        new Product(1,
            "Classic White T-Shirt",
            "100% cotton crew neck t-shirt, perfect for everyday wear",
            19.99),
        new Product(2,
            "Slim Fit Jeans",
            "Dark wash denim jeans with stretch for comfort",
            59.99),
        new Product(3,
            "Hooded Sweatshirt",
            "Fleece-lined hoodie with front pocket and adjustable drawstrings",
            44.99),
        new Product(4,
            "Oxford Button-Down Shirt",
            "Long-sleeve dress shirt in premium cotton",
            79.99),
        new Product(5,
            "Yoga Leggings",
            "High-waisted leggings with moisture-wicking fabric",
            49.99),
        new Product(6,
            "Wool Blend Coat",
            "Winter coat with thermal lining and waterproof exterior",
            129.99),
        new Product(7,
            "Running Shorts",
            "Lightweight shorts with built-in liner and reflective details",
            34.99),
        new Product(8,
            "Cashmere Sweater",
            "Soft, luxurious crew neck sweater for cold weather",
            89.99),
        new Product(9, "Chino Pants", "Casual trousers in khaki with modern slim fit", 54.99),
        new Product(10,
            "Maxi Dress",
            "Floral print dress with flowy silhouette and side slits",
            69.99),
        new Product(11, "Leather Jacket", "Genuine leather motorcycle-style jacket", 199.99),
        new Product(12,
            "Athletic Socks (3-pack)",
            "Breathable socks with arch support for sports",
            16.99),
        new Product(13,
            "Swim Trunks",
            "Quick-dry shorts with mesh liner and UPF 50 protection",
            39.99),
        new Product(14,
            "Silk Blouse",
            "Elegant blouse with French cuffs and delicate buttons",
            89.99),
        new Product(15,
            "Beanie Hat",
            "Knit wool hat for winter warmth in assorted colors",
            24.99)
    };
}