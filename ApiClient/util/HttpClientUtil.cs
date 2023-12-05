using System.Text.Json;

namespace ApiClient.util;

internal static class HttpClientUtil {
    private const string ApiKey = "77d2ee32ec04454772ae601035a1532e";


    internal static async Task<T> Get<T>(string url) {
        using var httpClient = new HttpClient();
        string requestUri = $"{url}?api_key={ApiKey}";
        var httpResponseMessage = await httpClient.GetAsync(requestUri);
        var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();

        if (!httpResponseMessage.IsSuccessStatusCode) {
            throw new Exception($"Error: {httpResponseMessage.StatusCode}, {responseContent}");
        }

        return GetDeserialized<T>(responseContent);
    }
    public static async Task<T> GetWithQuery<T>(string url) {
        using var httpClient = new HttpClient();
        string requestUri = $"{url}&api_key={ApiKey}";
        Console.WriteLine(requestUri);
        var httpResponseMessage = await httpClient.GetAsync(requestUri);
        var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();

        if (!httpResponseMessage.IsSuccessStatusCode) {
            throw new Exception($"Error: {httpResponseMessage.StatusCode}, {responseContent}");
        }

        return GetDeserialized<T>(responseContent);
    }

    private static T GetDeserialized<T>(string jsonString) {
       var deserialized = JsonSerializer.Deserialize<T>(jsonString);
       if (deserialized is null) {
           throw new Exception("Deserialized object is null");
       }

       return deserialized;
    }
}