using System.Text.Json;
using static MinimalApi.ProductEndpoint;
using static MinimalApi.ProductSeed;


// helper generate with AI for to check if JSON is malformed 
namespace MinimalApi
    
{
    partial class Helper
    {
        /// <summary>
        /// Data transfer object for creating or updating product information with flexible price handling.
        /// </summary>
        public class ProductDataConverter
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



    }

}
