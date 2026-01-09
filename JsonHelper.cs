using System.Text.Json;

// helper generate with AI for to check if JSON is malformed 
namespace MinimalApi
    
{
    partial class Helper
    {
        public static async Task<(T? value, IResult? error)> TryReadJsonBodyAsync<T>(HttpRequest request)
        {
            try
            {
                request.EnableBuffering();
                using var sr = new StreamReader(request.Body, leaveOpen: true);
                string body = await sr.ReadToEndAsync();
                request.Body.Position = 0;

                if (string.IsNullOrWhiteSpace(body))
                    return (default, BadRequest("Request body is empty"));

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                try
                {
                    T? obj = JsonSerializer.Deserialize<T>(body, options);
                    if (obj == null) return (default, BadRequest("Unable to parse JSON body"));
                    return (obj, null);
                }
                catch (JsonException jex)
                {
                    WriteLine($"\nMalformed JSON: {jex.Message}");
                    return (default, BadRequest("Malformed JSON in request body"));
                }
                catch (Exception ex)
                {
                    WriteLine($"\nUnexpected error deserializing JSON: {ex.Message}");
                    return (default, BadRequest("Invalid JSON in request body"));
                }
            }
            catch (Exception ex)
            {
                WriteLine($"\nError reading request body: {ex.Message}");
                return (default, BadRequest("Unable to read request body"));
            }
        }


    }
}
