using static MinimalApi.ProductEndpoint;

namespace MinimalApi;

/// <summary>Main entry point for Minimal API product service.</summary>
/// <para>Author: Eddie C.</para>
/// <para>Version: 1.0</para>
/// <para>Date: Jan. 09, 2026</para>
public class Program
{
    /// <summary>Configures and runs the web application.</summary>
    /// <param name="args">Command-line arguments.</param>
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/error");
        }
        else
        {
            app.UseExceptionHandler("/error");
        }

        app.MapGet("/error", () => "Sorry, an error occurred");
 
        app.MapGet("/product/show/all", ShowAllProducts);
        
        app.MapGet("/product/search/id/{productId}", SearchById);

        app.MapDelete("/product/delete/{productId}", DeleteProduct);

        app.MapPost("/product/add/", AddProduct);

        app.Run();
    }
}
