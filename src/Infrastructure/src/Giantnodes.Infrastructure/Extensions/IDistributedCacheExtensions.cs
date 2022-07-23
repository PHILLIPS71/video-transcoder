using System.Text.Json;

namespace Microsoft.Extensions.Caching.Distributed
{
    public static class IDistributedCacheExtensions
    {
        public static async Task<T?> GetAsync<T>(this IDistributedCache cache, string key, CancellationToken cancellation = default)
            where T : class
        {
            var result = await cache.GetStringAsync(key, cancellation);
            if (result == null)
                return null;

            return JsonSerializer.Deserialize<T>(result);
        }

        public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, TimeSpan duration, CancellationToken token = default)
            where T : class
        {
            await SetAsync(cache, key, value, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = duration }, token);
        }

        private static async Task SetAsync<T>(IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default)
            where T : class
        {
            await cache.SetStringAsync(key, JsonSerializer.Serialize(value), options, token);
        }
    }
}
