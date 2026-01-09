namespace MinimalApi.Support;

/// <summary> Represents a product in the clothing store inventory. </summary>
/// <remarks> This class defines product data structure with both parameterized and
/// parameterless constructors for serialization support. </remarks>
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }

    // Parameterless constructor for JSON deserialization
    public Product()
    {
    }

    // Constructor with parameters
    public Product(int productId, string name, string description, double price)
    {
        ProductId = productId;
        Name = name;
        Description = description;
        Price = price;
    }
}

/// <summary> Initializes a new instance of the <see cref="Product"/> class with specified
/// parameters. </summary>
/// <param name="productId">The unique identifier for the product.</param>
/// <param name="name">The name of the product.</param>
/// <param name="description">The detailed description of the product.</param>
/// <param name="price">The price of the product.</param>
/// <remarks> This constructor was created with help from AI to provide a convenient way to
/// instantiate products with initial values. </remarks>
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


