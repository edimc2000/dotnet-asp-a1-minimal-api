using System.Text.Json;
using static MinimalApi.Support.ProductSeed;
using System.ComponentModel.DataAnnotations;
using static MinimalApi.Helper;
using MinimalApi.Support;

namespace MinimalApi;

/// <summary>Endpoint handlers for product operations in Minimal API.</summary>
/// <remarks>Manages product CRUD operations using in-memory clothing products collection.</remarks>
/// <para>Author: Eddie C.</para>
/// <para>Version: 1.0</para>
/// <para>Date: Jan. 09, 2026</para>
public class ProductEndpoint
{
    /// <summary>Retrieves all products from the collection.</summary>
    /// <returns>Success result with all products.</returns>
    public static IResult ShowAllProducts()
    {
        ConsoleDebugging(null, "ShowAllProducts");
        return SearchAllSuccess();
    }

    /// <summary>Searches for a product by its ProductId (numeric).</summary>
    /// <param name="productId">String representation of product ID.</param>
    /// <returns>Product if found, BadRequest or NotFound otherwise.</returns>
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

    /// <summary>Deletes a product from the collection by ID.</summary>
    /// <param name="productId">String representation of product ID to delete.</param>
    /// <returns>Success if deleted, BadRequest or NotFound otherwise.</returns>
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

    /// <summary>Adds a new product from JSON request body.</summary>
    /// <param name="request">HTTP request containing product JSON data.</param>
    /// <returns>Success with new product or error result.</returns>
    public static async Task<IResult> AddProduct(HttpRequest request)
    {
        int lastProductId = clothingProducts.Max(p => p.ProductId);
        int productId = lastProductId + 1;
        ConsoleDebugging(null, "AddProduct");
     

        (ProductDataConverter? dataConverter, IResult? error) =
            await TryReadJsonBodyAsync<ProductDataConverter>(request);
        if (error != null) return error;

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

        // Data annotation validation
        var validationContext = new ValidationContext(newProduct);
        var validationResults = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(newProduct, validationContext, validationResults, true);
        if (!isValid)
        {
            var errors = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
            return BadRequest($"Validation failed: {errors}");
        }

        clothingProducts.Add(newProduct);
        return AddSuccess(productId, newProduct);
    }
    
}