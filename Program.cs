using static MinimalApi.ProductSeed;
using static MinimalApi.ProductEndpoint;


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
        List<Person> people = new()
        {
            new Person("Tom", "Hanks"),
            new Person("Tommy", "Walsch"),
            new Person("Denzel", "Washington"),
            new Person("Leondardo", "DiCaprio"),
            new Person("Al", "Pacino"),
            new Person("Morgan", "Freeman")
        };
        app.MapGet("/person/{name}",
            (string name) =>
            {
                WriteLine(name);
                return people.Where(p => p.FirstName.StartsWith(name));
            }
        );


        // A1  - Get 

        // ** requirement 1  ** Test 2x
        app.MapGet("/product/show/all", ShowAllProducts);


        // ** requirement 2  ** Test 3x
        app.MapGet("/product/search/id/{productId}", SearchById);

        // lower priority 
        app.MapGet("/product/search/name/{productname}", SearchByName);


        app.MapPost("/product/add/{productId}", AddProduct);

        app.MapDelete("/product/delete/{productId}", DeleteProduct);

        app.MapGet("/products/count", () => $"Total products: {clothingProducts.Count}");


        app.Run();
    }

    public record Person(string FirstName, string LastName);
}