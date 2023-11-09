using System.Text.Json;
namespace TokoWebApp.Helper
{
    public static class HttpClientExtension
    {
        public static async Task<T> ReadContentAsync<T>(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var result = JsonSerializer.Deserialize<T>(
                    data, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                return result;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
