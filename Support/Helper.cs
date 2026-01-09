using MinimalApi.Support;
using System.Text.Json;
using static MinimalApi.Support.ProductSeed;

namespace MinimalApi;

/// <summary>Helper methods for product API operations.</summary>
/// <para>Author: Eddie C.</para>
/// <para>Version: 1.0</para>
/// <para>Date: Jan. 09, 2026</para>
internal partial class Helper
{
    /// <summary>DTO for flexible product data conversion.</summary>
    /// <remarks>Uses JsonElement to accept any JSON type for price.</remarks>
    internal class ProductDataConverter
    {
        public JsonElement Name { get; set; }
        public JsonElement Description { get; set; }
        public JsonElement Price { get; set; } 
    }

    /// <summary>Returns formatted bad request response.</summary>
    /// <param name="message">Error message.</param>
    public static IResult BadRequest(string message)
    {
        return Results.BadRequest(new
        {
            success = false,
            message = $"{message}"
        });
    }
    
    /// <summary>Returns formatted not found response.</summary>
    /// <param name="productId">Missing product ID.</param>
    public static IResult NotFound(int productId)
    {
        return Results.NotFound(new
        {
            success = false,
            message = $"Product with ID {productId} not found"
        });
    }

    /// <summary>Returns successful delete response.</summary>
    /// <param name="product">Deleted product.</param>
    public static IResult DeleteSuccess(Product product)
    {
        return Results.Ok(new
        {
            success = true,
            message = "Product deleted",
            data = product
        });
    }

    /// <summary>Returns successful product search response.</summary>
    /// <param name="productId">Found product ID.</param>
    public static IResult SearchSuccess(int productId)
    {
        return Results.Ok(new
        {
            success = true,
            message = $"Product with ID {productId} found",
            data = clothingProducts.Where(p => p.ProductId == productId).ToList()
        });
    }

    /// <summary>Returns successful all products response.</summary>
    public static IResult SearchAllSuccess()
    {
        return Results.Ok(new
        {
            success = true,
            message = $"Total of {clothingProducts.Count} products retrieved successfully",
            data = clothingProducts.ToList()
        });
    }

    /// <summary>Returns successful add response with location.</summary>
    /// <param name="productId">New product ID.</param>
    /// <param name="newProduct">Added product.</param>
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

    /// <summary>Converts JsonElement to string based on value kind. AI - Deepseek
    /// assistance used</summary>
    /// <param name="element">JSON element to convert.</param>
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


    /// <summary>Outputs debug info to console for API operations.</summary>
    /// <param name="productId">Product ID being processed.</param>
    /// <param name="task">Operation type.</param>
    internal static void ConsoleDebugging (string productId, string task)
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