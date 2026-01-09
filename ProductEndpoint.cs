using System.Text.Json;
using static MinimalApi.ProductSeed;
using static MinimalApi.Helper;

namespace MinimalApi;

public class ProductEndpoint
{
    public static IResult ShowAllProducts()
    {
        ConsoleDebugging(null, "ShowAllProducts");
        return SearchAllSuccess();
    }

    public static IResult SearchById(string productId)
    {
        ConsoleDebugging(productId, "SearchById");
        if (!int.TryParse(productId, out int id))
        {
            ConsoleDebugging(productId, "ParseFailed");
            return BadRequest($"'{productId}' is not a valid product ID");
        }

        ConsoleDebugging(productId, "ParseSuccess");
        Product product = clothingProducts.FirstOrDefault(p => p.ProductId == id);

        if (product == null) return NotFound(id);
        return SearchSuccess(id);
    }


    public static IResult DeleteProduct(string productId)
    {

        ConsoleDebugging(productId, "DeleteProduct");

        if (!int.TryParse(productId, out int id))
        {
            ConsoleDebugging(productId, "ParseFailed");
            return BadRequest($"'{productId}' is not a valid product ID");
        }

        ConsoleDebugging(productId, "ParseSuccess");
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

        (ProductDataConverter? dataConverter, IResult? error) =
            await TryReadJsonBodyAsync<ProductDataConverter>(request);
        if (error != null) return error;

        // dataConverter is non-null here
        double price;
        try
        {
            if (dataConverter!.Price.ValueKind == JsonValueKind.String)
            {
                WriteLine($"\nprice data type {dataConverter.Price.ValueKind}");
                price = double.Parse(dataConverter.Price.GetString()!);
            }
            else if (dataConverter.Price.ValueKind == JsonValueKind.Number)
            {
                price = dataConverter.Price.GetDouble();
            }
            else
            {
                throw new FormatException("Invalid price format");
            }
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
    
}