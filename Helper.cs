using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static MinimalApi.ProductEndpoint;
//using static MinimalApi.ProductSeed;

namespace MinimalApi


{
    public class Helper
    {

        /// <summary>
        /// Data transfer object for creating or updating product information with flexible price handling.
        /// </summary>
        public class ProductDataConverter
        {
            public JsonElement Name
            {
                get; set;
            }
            public JsonElement Description
            {
                get; set;
            }
            public JsonElement Price
            {
                get; set;
            } // Use JsonElement to accept any JSON type
        }


        // new helper: read body and try deserialize, returning either the value or an IResult error
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
