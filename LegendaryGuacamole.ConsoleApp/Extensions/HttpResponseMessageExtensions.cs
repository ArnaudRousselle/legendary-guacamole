using System.Net.Http.Json;

namespace LegendaryGuacamole.ConsoleApp.Extensions;

public static class HttpResponseMessageExtensions
{
    public async static Task ContinueWithAsync<TOutput>(this HttpResponseMessage response, Action<TOutput> action)
        where TOutput : class
    {
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Wrong status code: " + response.StatusCode);
            return;
        }

        var output = await response.Content.ReadFromJsonAsync<TOutput>();

        if (output == null)
        {
            Console.WriteLine("Invalid data");
            return;
        }

        action(output);
    }
}