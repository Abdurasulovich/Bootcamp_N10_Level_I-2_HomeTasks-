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
            var currentEntryOptions = _entryOpitons;
            if (entryOptions != default)
            {
                currentEntryOptions = _entryOpitons.DeepClone();
                _entryOpitons.AbsoluteExpirationRelativeToNow = entryOptions.AbsoluteExpirationRelativeToNow;
                _entryOpitons.SlidingExpiration = entryOptions.SlidingExpiration;
            }

            return await appCache.GetOrAddAsync(key, valueFactory, _entryOpitons);
        }

        public ValueTask SetAsync<T>(string key, T value, CacheEntryOptions? entryOptions= default)
        {
            var currentEntryOptions = _entryOpitons;
            if (entryOptions != default)
            {
                currentEntryOptions = _entryOpitons.DeepClone();
                _entryOpitons.AbsoluteExpirationRelativeToNow = entryOptions.AbsoluteExpirationRelativeToNow;
                _entryOpitons.SlidingExpiration = entryOptions.SlidingExpiration;
            }
            appCache.Add(key, value, _entryOpitons);

            return ValueTask.CompletedTask;
        }
    }
}
