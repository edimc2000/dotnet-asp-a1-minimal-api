using static MinimalApi.ProductEndpoint;

namespace MinimalApi;

public class Program
{
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
