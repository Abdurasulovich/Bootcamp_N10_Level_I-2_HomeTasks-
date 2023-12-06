using Caching.SimpleInfra.Domain.Common.Caching;
using Caching.SimpleInfra.Infrastructure.Common.Settings;
using Caching.SimpleInfra.Persistence.Caching;
using Force.DeepCloner;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Caching.SimpleInfra.Infrastructure.Common.Caching
{
    public class LazyMemoryCacheBroker(IAppCache appCache, IOptions<CacheSettings> cacheSettings) : ICacheBroker
    {
        private readonly MemoryCacheEntryOptions _entryOpitons = new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheSettings.Value.AbsoluteExpirationInSeconds),
            SlidingExpiration = TimeSpan.FromSeconds(cacheSettings.Value.SlidingExpirationInSeconds)
        };
        public ValueTask DeleteAsync(string key)
        {
            appCache.Remove(key);

            return ValueTask.CompletedTask;
        }

        public async ValueTask<T> GetAsync<T>(string key) => await appCache.GetAsync<T>(key);

        public async ValueTask<T> GetOrSetAsync<T>(string key, Func<Task<T>> valueFactory, CacheEntryOptions? entryOptions=default)
        {
            return await appCache.GetOrAddAsync(key, valueFactory, GetCacheEntryOptions(entryOptions));
        }

        public ValueTask SetAsync<T>(string key, T value, CacheEntryOptions? entryOptions= default)
        {
            appCache.Add(key, value, GetCacheEntryOptions(entryOptions));

            return ValueTask.CompletedTask;
        }

        public MemoryCacheEntryOptions GetCacheEntryOptions(CacheEntryOptions? entryOptions)
        {
            if (entryOptions == default || (!entryOptions.AbsoluteExpirationRelativeToNow.HasValue && !entryOptions.SlidingExpiration.HasValue))
                return _entryOpitons;

            var currentEntryOptions = _entryOpitons.DeepClone();

            currentEntryOptions.AbsoluteExpirationRelativeToNow = entryOptions.AbsoluteExpirationRelativeToNow;
            currentEntryOptions.SlidingExpiration = entryOptions.SlidingExpiration;

            return currentEntryOptions;
        }

        public ValueTask<bool> TryGetAsync<T>(string key, out T value)
        {
            return new ValueTask<bool>(appCache.TryGetValue(key, out value));
        }
    }
}
