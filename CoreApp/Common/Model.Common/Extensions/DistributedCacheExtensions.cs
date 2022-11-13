using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Model.Common.Extensions;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Model.Common.Extensions
{
    public static class DistributedCacheExtensions
    {
        // Make sure to adjust these values to suit your own defaults...
        public static readonly DistributedCacheEntryOptions DefaultDistributedCacheEntryOptions
            = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
                SlidingExpiration = TimeSpan.FromSeconds(10),
            };

        public static Task SetAsync<T>(this IDistributedCache cache, string key, T value) where T : class
        {
            return cache.SetAsync(key, value, DefaultDistributedCacheEntryOptions);
        }

        public static Task SetAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options) where T : class
        {
            var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, GetJsonSerializerOptions()));
            return cache.SetAsync(key, bytes, options);
        }

        public static async Task<T?> GetAsync<T>(this IDistributedCache cache, string key) where T : class
        {
            var bytes = await cache.GetAsync(key);
            if (bytes == null) return default;
            return JsonSerializer.Deserialize<T>(bytes, GetJsonSerializerOptions());
        }

        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                PropertyNamingPolicy = null,
                WriteIndented = true,
                AllowTrailingCommas = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };
        }
    }
}
