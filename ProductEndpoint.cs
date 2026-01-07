using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static MinimalApi.ProductSeed;


namespace MinimalApi;

public class ProductEndpoint
{
    public static List<Product> ShowAllProducts()
    {
        return clothingProducts.ToList();
    }

    private static List<Product> ReturnEmpty()
    {
        return new List<Product>
        {
            new Product(
                -1, // Or -1 for "not found" ID
                "No products foundxxx",
                $"No products found",
                0.00
            )
        };
    }

    public static List<Product> SearchById(int productid)
    {
        WriteLine($"Searching for product ID: {productid}");
        Product product = clothingProducts.FirstOrDefault(p => p.ProductId == productid);
        //Product product = clothingProducts.whe(p => p.ProductId == productid);

        if (product == null)
        {
            return ReturnEmpty(); 
            //throw new KeyNotFoundException($"Product with ID {product_id} not found"); /// implement this with the name as well 
            //return TypedResults.NotFound($"Product with ID {product_id} not found");
        }

        return product != null ? new List<Product> { product } : new List<Product>();
    }

    public static List<Product> SearchByName(string productname)
    {
        WriteLine($"Searching for products starting with: {productname}");
        // Where() returns IEnumerable, not null, so we need to check if it's empty
        var products = clothingProducts.Where(p => p.Name.StartsWith(productname, StringComparison.OrdinalIgnoreCase));

        // Convert to List and check if it's empty
        var result = products.ToList();

        if (!result.Any()) // Check if list is empty, not null
        {
            return ReturnEmpty(); 
        }

        return result;
    }


    public static IResult AddProduct(int productid, Product product)
    {
        WriteLine($"id: {productid}");
        WriteLine($"Name: {product.Name}");
        WriteLine($"Description: {product.Description}");
        WriteLine($"Price: {product.Price}");
    

        // add logic to check if id exists if exist no creation should happen 
        
        // Add your logic to save the product
        var newProduct = new Product(productid,
            product.Name,
            product.Description,
            product.Price);
    
        // Add the product to the list
        clothingProducts.Add(newProduct);


        //return Results.Ok($"Product {productid} added successfully");
        //return Results.Created();
        //Uri should be the location where the record can be validated 
        return Results.Created($"/product/search/{productid}", new 
        {
            Message = $"Product {productid} added successfully",
            ProductId = productid,
            Timestamp = DateTime.UtcNow, 
            newProduct

        });
    }



}