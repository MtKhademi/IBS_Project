using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net.Http.Json;

namespace Common.Extentions
{
    public static class HttpContentExtentions
    {
        public static StringContent ToContentHttpString(this object o)
               => new StringContent(JsonSerializer.Serialize(o), Encoding.UTF8, "application/json");

        public static async Task<T> ReadModelFromJsonAsync<T>(this HttpContent content)
        {
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            options.Converters.Add(new JsonStringEnumConverter());
            return await content.ReadFromJsonAsync<T>(options);
        }

    }
}
