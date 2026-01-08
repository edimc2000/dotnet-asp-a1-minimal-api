using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static MinimalApi.ProductSeed;


namespace MinimalApi;

public class ProductEndpoint
{
    // ** Final 
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

    
    public static IResult BadRequest(string productId)
    {
        return Results.BadRequest(new
        {
            success = false,
            error = "INVALID_INPUT",
            message = $"'{productId}' is not a valid product ID"
        });
    }

    public static IResult NotFound(int productId)
    {
        return Results.NotFound(new
        {
            success = false,
            error = "PRODUCT_NOT_FOUND",
            message = $"Product with ID {productId} not found"
        });
    }


    // testing is good 2 tests requirement # 1 
    public static ApiResult<List<Product>> ShowAllProducts()
    {
        return ApiResult<List<Product>>.SuccessResult(
            clothingProducts.ToList(),
            $"Total of {clothingProducts.Count} products retrieved successfully"
        );
    }
    private static ApiResult<List<Product>> ReturnError()
    {
        return ApiResult<List<Product>>.Failure(
            "Search requires an integer value",
            "INVALID_PRODUCT_ID"
        );
    }

    public static IResult SearchById(string productId)
    {
        WriteLine($"---\nType of productId parameter: {productId?.GetType()?.Name ?? "null"}");
        WriteLine($"Value received: '{productId}'");

        // This method now handles parsing internally and returns IResult
        if (!int.TryParse(productId, out int id))
        {
            WriteLine($"Error: Could not parse '{productId}' to integer");
            return BadRequest(productId);
        }

        WriteLine($"Successfully parsed to int: {id}");
        Product product = clothingProducts.FirstOrDefault(p => p.ProductId == id);

        if (product == null) return NotFound(id);

        // Return success with ApiResult format
        return Results.Ok(new ApiResult<List<Product>>
        {
            Success = true,
            Message = $"Product with ID {id} found",
            Error = "",
            Data = clothingProducts.Where(p => p.ProductId == id).ToList()
        });
    }


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


    public static IResult AddProduct(int productid, Product product)
    {
        //debugging 
        WriteLine($"id: {productid}");
        WriteLine($"Name: {product.Name}");
        WriteLine($"Description: {product.Description}");
        WriteLine($"Price: {product.Price}");


        // add logic to check if id exists if exist no creation should happen 

        // Add your logic to save the product
        Product newProduct = new(productid,
            product.Name,
            product.Description,
            product.Price);

        // Add the product to the list
        clothingProducts.Add(newProduct);


        //return Results.Ok($"Product {productid} added successfully");
        //return Results.Created();
        //Uri should be the location where the record can be validated 
        return Results.Created($"/product/search/{productid}",
            new
            {
                Message = $"Product {productid} added successfully",
                ProductId = productid,
                Timestamp = DateTime.UtcNow,
                newProduct
            });
    }


    public static IResult DeleteProduct(string productId)
    //public static void  DeleteProduct(string productId)
    {
        
       
        WriteLine($"---\nType of productId parameter: {productId?.GetType()?.Name ?? "null"}");
        WriteLine($"Value received: '{productId}'");
        WriteLine($"Attempting to delete product ID: {productId}");


        if (!int.TryParse(productId, out int id))
        {
            WriteLine($"Error: Could not parse '{productId}' to integer");
            //return BadRequest(productId);
        }

        WriteLine($"Successfully parsed to int: {id}");
        //Product product = clothingProducts.FirstOrDefault(p => p.ProductId == id);
        Product? productToDelete = clothingProducts.FirstOrDefault(p => p.ProductId == id);

        if (productToDelete == null)
            // Option 1: Return 404 (item doesn't exist)
            //return Results.NotFound($"Product with ID {productId} not found");
            return NotFound(id);
        // Option 2: Return 204 anyway (idempotent - same result regardless)
        // return Results.NoContent();
        bool removed = clothingProducts.Remove(productToDelete);

        if (removed)
            // Option 1: Standard REST - 204 No Content
            //return Results.NoContent();
            // Option 2: Return 200 with message
            // return Results.Ok($"Product {productid} deleted successfully");
            //Option 3: Return the deleted product
            return Results.Ok(new
            {
                message = "Product deleted",
                deletedProduct = productToDelete
            });

        return Results.Problem("Failed to delete product");
    }
}

public class ApiResult<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
    public T? Data { get; set; }

    public static ApiResult<T> Failure(string message, string error = "")
    {
        return new ApiResult<T>
        {
            Success = false,
            Message = message,
            Error = error,
            Data = default
        };
    }

    public static ApiResult<T> SuccessResult(T data, string message = "")
    {
        return new ApiResult<T>
        {
            Success = true,
            Message = message,
            Error = string.Empty,
            Data = data
        };
    }
}