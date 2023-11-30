using System.Text.Json;
using System.Text.Json.Nodes;

namespace ApiClient.util;

public class HttpClientUtil {
    private const string ApiKey = "77d2ee32ec04454772ae601035a1532e";


    public static async Task<T> Get<T>(string url) {
        using var httpClient = new HttpClient();
        string requesrUri = $"{url}?api_key={ApiKey}";

        var httpResponseMessage = await httpClient.GetAsync(requesrUri);
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