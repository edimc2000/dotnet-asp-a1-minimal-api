using MinimalApi.Support;
using System.Text.Json;
using static MinimalApi.Support.ProductSeed;


// helper generate with AI for to check if JSON is malformed 
namespace MinimalApi;

internal partial class Helper
{
    /// <summary>
    /// Data transfer object for creating or updating product information with flexible price handling.
    /// </summary>
    internal class ProductDataConverter
    {
        public JsonElement Name { get; set; }
        public JsonElement Description { get; set; }
        public JsonElement Price { get; set; } // Use JsonElement to accept any JSON type
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


    internal static string ConvertJsonElementToString(JsonElement? element)
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

    internal static void ConsoleDebugging
    (
        string productId, string task
    )
    {
        switch (task)
        {
            case "ShowAllProducts":
                WriteLine("\n" + new string('-', 80) + $"\nRETRIEVE ALL PRODUCTS");
                break;

            case "SearchById":
                WriteLine("\n" + new string('-', 80) + $"\nSEARCH PRODUCT" +
                          $"\nType of productId parameter: {productId?.GetType()?.Name ?? "null"}" +
                          $"\nValue received: '{productId}'");
                break;

            case "ParseFailed":
                WriteLine($"Error: Could not parse '{productId}' to integer");
                break;

            case "ParseSuccess":
                WriteLine($"Successfully parsed to int: {productId}");
                break;

            case "DeleteProduct":
                WriteLine("\n" + new string('-', 80) +
                          $"\nDELETE PRODUCT" +
                          $"\nType of productId parameter: {productId?.GetType()?.Name ?? "null"}" +
                          $"\nValue received: '{productId}'");
                break;

            case "AddProduct":
                WriteLine("\n" + new string('-', 80) + $"\nADD PRODUCT");
                break;

            default:
                break;
        }
    }
}