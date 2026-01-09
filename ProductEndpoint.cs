using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static MinimalApi.ProductEndpoint;
using static MinimalApi.ProductSeed;
using static MinimalApi.Helper;


namespace MinimalApi;

public class ProductEndpoint
{
    public class ProductX
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Price { get; set; }

        // Parameterless constructor for JSON deserialization
        public ProductX()
        {
        }

        // Constructor with parameters
        public ProductX(int productId, string name, string description, string price)
        {
            ProductId = productId;
            Name = name;
            Description = description;
            Price = price;
        }
    }


    // **    used for serching by name 
    private static List<Product> ReturnEmpty()
    {
        return new List<Product>
        {
            new(
                -1, // value if no products were found 
                "No products found",
                $"No products found",
                0.00
            )
        };
    }


    public static IResult BadRequest(string message)
    {
        return Results.BadRequest(new
        {
            success = false,
            //message = $"'{productId}' is not a valid product ID"
            message = $"{message}"
        });
    }

    public static IResult NotFound(int productId)
    {
        return Results.NotFound(new
        {
            success = false,
            message = $"Product with ID {productId} not found"
        });
    }


    public static IResult DeleteSuccess(Product product)
    {
        return Results.Ok(new
        {
            success = true,
            message = "Product deleted",
            data = product
        });
    }


    public static IResult SearchSuccess(int productId)
    {
        return Results.Ok(new
        {
            success = true,
            message = $"Product with ID {productId} found",
            data = clothingProducts.Where(p => p.ProductId == productId).ToList()
        });
    }


    public static IResult SearchAllSuccess()
    {
        return Results.Ok(new
        {
            success = true,
            message = $"Total of {clothingProducts.Count} products retrieved successfully",
            data = clothingProducts.ToList()
        });
    }

    public static IResult AddSuccess(int productId, Product newProduct)
    {
        return Results.Created($"/product/search/id/{productId}",
            new
            {
                success = true,
                Message =
                    $"Product {productId} added successfully retrieve using /product/search/id/{productId}",
                data = newProduct
            });
    }

    // testing is good 2 tests requirement # 1 
    public static IResult ShowAllProducts()
    {
        WriteLine("\n" + new string('-', 80) +
                  $"\nRETRIEVE ALL");
        return SearchAllSuccess();
    }

    // ** requirement 2  search for product using ProductId ** Test 3x
    public static IResult SearchById(string productId)
    {
        WriteLine("\n" + new string('-', 80) +
                  $"\nSEARCH" +
                  $"\nType of productId parameter: {productId?.GetType()?.Name ?? "null"}" +
                  $"\nValue received: '{productId}'");

        // This method now handles parsing internally and returns IResult
        if (!int.TryParse(productId, out int id))
        {
            WriteLine($"Error: Could not parse '{productId}' to integer");
            return BadRequest($"'{productId}' is not a valid product ID");
        }

        WriteLine($"Successfully parsed to int: {id}");
        Product product = clothingProducts.FirstOrDefault(p => p.ProductId == id);

        if (product == null) return NotFound(id);

        return SearchSuccess(id);
    }

    // ** requirement 2 delete product using ProductId 
    public static IResult DeleteProduct(string productId)
    {
        WriteLine("\n" + new string('-', 80) +
                  $"\nDELETE" +
                  $"\nType of productId parameter: {productId?.GetType()?.Name ?? "null"}" +
                  $"\nValue received: '{productId}'");

        if (!int.TryParse(productId, out int id))
        {
            WriteLine($"Error: Could not parse '{productId}' to integer");
            return BadRequest($"'{productId}' is not a valid product ID");
        }

        WriteLine($"Successfully parsed to int: {id}");
        Product? productToDelete = clothingProducts.FirstOrDefault(p => p.ProductId == id);

        if (productToDelete == null)
            return NotFound(id);

        bool removed = clothingProducts.Remove(productToDelete);

        if (removed)
            return DeleteSuccess(productToDelete);

        return Results.Problem();
    }

    
    // updated AddProduct to use the helper

    public static async Task<IResult> AddProduct(HttpRequest request)
    {
        int lastProductId = clothingProducts.Max(p => p.ProductId);
        int productId = lastProductId + 1;

        WriteLine("lastProductId: " + lastProductId);
        WriteLine($"id: {productId}");
        WriteLine("\n" + new string('-', 80) + $"\nADD");

        var (dataConverter, error) = await TryReadJsonBodyAsync<ProductDataConverter>(request);
        if (error != null) return error;

        // dataConverter is non-null here
        double price;
        try
        {
            if (dataConverter!.Price.ValueKind == JsonValueKind.String)
            {
                WriteLine($"\nprice data type { dataConverter.Price.ValueKind }");
                price = double.Parse(dataConverter.Price.GetString()!);
            }
            else if (dataConverter.Price.ValueKind == JsonValueKind.Number)
                price = dataConverter.Price.GetDouble();
            else
                throw new FormatException("Invalid price format");
        }
        catch (Exception)
        {
            return BadRequest("Invalid price format. Price must be a valid number.");
        }

        WriteLine($"\nType of price parameter: {price.GetType()?.Name ?? "null"}" +
                  $"\nValue received: '{price}'");

        Product newProduct = new()
        {
            ProductId = productId,
            Name = ConvertJsonElementToString(dataConverter.Name),
            Description = ConvertJsonElementToString(dataConverter.Description),
            Price = price
        };

        clothingProducts.Add(newProduct);
        return AddSuccess(productId, newProduct);
    }


    //public static IResult xxxxAddProduct1([FromBody] Product? product)
    //{
    //    if (product == null) return BadRequest("Product data is required");


    //    // Validate required fields
    //    if (product.Price.GetType().Name == "String")
    //    {
    //        WriteLine("\n" + new string('-', 80) +
    //                  $"\nADD");
    //        return Results.BadRequest("Price should be a double");
    //    }

    //    WriteLine("\n" + new string('-', 80) +
    //              $"\nADD" +
    //              $"\nType of productId parameter: {product.Price.GetType()?.Name ?? "null"}" +
    //              $"\nValue received: '{product.Price}'");


    //    //if (!double.TryParse(product.Price, out double priceX))
    //    //{
    //    //    WriteLine($"Error: Could not parse '{product.Price}' to double (correct format)");
    //    //    return BadRequest("Price should be a double");
    //    //}


    //    int lastProductId = clothingProducts.Max(p => p.ProductId);
    //    int productId = lastProductId + 1;

    //    //debugging 
    //    WriteLine("\n" + new string('-', 80) +
    //              $"\nADD");
    //    WriteLine("lastProductId: " + lastProductId);
    //    WriteLine($"id: {productId}");
    //    WriteLine($"Name: {product.Name}");
    //    WriteLine($"Description: {product.Description}");
    //    WriteLine($"Price: {product.Price}");


    //    // add logic to check if id exists if exist no creation should happen 

    //    // Add your logic to save the product
    //    Product newProduct = new(productId,
    //        product.Name,
    //        product.Description,
    //        product.Price);

    //    // Add the product to the list
    //    clothingProducts.Add(newProduct);

    //    return AddSuccess(productId, newProduct);
    //    //return Results.Ok();
    //}


    // it may return multiple records 
    public static List<Product> SearchByName(string productname)
    {
        WriteLine($"Searching for products starting with: {productname}");
        // Where() returns IEnumerable, not null, so we need to check if it's empty
        IEnumerable<Product> products = clothingProducts.Where(p =>
            p.Name.StartsWith(productname, StringComparison.OrdinalIgnoreCase));

        // Convert to List and check if it's empty
        List<Product> result = products.ToList();

        if (!result.Any()) // Check if list is empty, not null
            return ReturnEmpty();

        return result;
    }



    private static string ConvertJsonElementToString(JsonElement? element)
    {
        if (!element.HasValue) return string.Empty;
    
        return element.Value.ValueKind switch
        {
            JsonValueKind.String => element.Value.GetString() ?? string.Empty,
            JsonValueKind.Number => element.Value.GetRawText(),
            JsonValueKind.True => "true",
            JsonValueKind.False => "false",
            JsonValueKind.Null => string.Empty,
            _ => element.Value.ToString()
        };
    }
}
