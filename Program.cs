using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using static MinimalApi.ProductEndpoint;
using static MinimalApi.ProductSeed;


namespace MinimalApi;

public class Program
{
    public class PersonResponse
    {
        public string Message { get; set; }
        public int Number { get; set; }
    }

    public static void Main(string[] args)
    {
        // Set environment variable BEFORE creating builder
        //Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        WebApplication app = builder.Build();

        WriteLine($"ENVIRONMENT DEV: {app.Environment.IsDevelopment()}");
        if (app.Environment.IsDevelopment())
        {
            //app.UseDeveloperExceptionPage();
            //app.UseExceptionHandler("/error");
            //app.UseStatusCodePages();
            app.UseExceptionHandler("/error");
        }
        else
        {
            app.UseExceptionHandler("/error");
        }

        app.MapGet("/error", () => "Sorry, an error occurred");
        // A1  - Get 

        // ** requirement 2 show all products   ** Test 2x
        app.MapGet("/product/show/all", ShowAllProducts);


        // ** requirement 2  search for product using ProductId ** Test 3x
        app.MapGet("/product/search/id/{productId}", SearchById);

        // ** requirement 2 delete product using ProductId 
        app.MapDelete("/product/delete/{productId}", DeleteProduct);


        //app.MapPost("/product/add/", AddProduct);


        //app.MapPost("/product/add/",


        //    ([FromBody] Product? product) => // Add nullable type
        //    {

        //        WriteLine("\n" + new string('-', 80) + $"\nADD");
        //        if (product == null)
        //        {
        //            return Results.BadRequest("Product data is required");
        //        }

        //        // Process the product...
        //        return Results.Ok();
        //    }
        //);
        //*********************************************
   


        //*********************************************

        app.MapPost("/product/add/", AddProduct);


        app.MapPost("/product/v2/add/{productId}", AddProduct);


        app.MapGet("/products/count", () => $"Total products: {clothingProducts.Count}");

        // lower priority 
        app.MapGet("/product/search/name/{productname}", SearchByName);



        //app.MapPost("/product/add/",
        //    ([FromBody] ProductDataConverter ?  dto) =>
        //    {
        //        WriteLine("\n" + new string('-', 80) + $"\nADD");
        
        //        if (dto == null)
        //        {
        //            return Results.BadRequest("Product data is required");
        //        }

        //        // Validate and parse price
        //        if (string.IsNullOrEmpty(dto.Price) || !double.TryParse(dto.Price, out double price))
        //        {
        //            return Results.BadRequest("Invalid price format. Price must be a valid number.");
        //        }

        //        // Create the actual Product object
        //        var product = new Product
        //        {
        //            Name = dto.Name,
        //            Description = dto.Description,
        //            Price = price
        //        };

        //        // Process the product...
        //        return Results.Ok("Product added successfully");
        //    }
        //);

        //app.MapPost("/product/add/",
        //    ([FromBody] ProductDataConverter ?  dataConverter) =>
        //    {
        //        WriteLine("\n" + new string('-', 80) + $"\nADD");
        
        //        if (dataConverter == null)
        //        {
        //            return BadRequest("Product data is required");
        //        }

        //        double price;
        //        try
        //        {
        //            // Try to parse the price from JsonElement
        //            if (dataConverter.Price.ValueKind == JsonValueKind.String)
        //            {
        //                price = double.Parse(dataConverter.Price.GetString()!);
        //            }
        //            else if (dataConverter.Price.ValueKind == JsonValueKind.Number)
        //            {
        //                price = dataConverter.Price.GetDouble();
        //            }
        //            else
        //            {
        //                throw new FormatException("Invalid price format");
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            return BadRequest("Invalid price format. Price must be a valid number.");
        //        }

        //        // Create the actual Product object
        //        Product product = new Product
        //        {
        //            Name = dataConverter.Name,
        //            Description = dataConverter.Description,
        //            Price = price
        //        };
        //        clothingProducts.Add(product);
        //        // Process the product...
        //        return Results.Ok($"Product added successfully. Price: {price}");
        //    }
        //);





        app.Run();
    }
}
