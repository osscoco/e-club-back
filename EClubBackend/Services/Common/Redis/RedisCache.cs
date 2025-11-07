using E_Club.Application.Interfaces.Common;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace E_Club.Services.Common.Redis
{
    public class RedisCache : IRedisCache
    {
        #region Attributs
        private readonly IDistributedCache _cache;
        #endregion

        #region Constructeur
        public RedisCache(IDistributedCache cache)
        {
            _cache = cache;
        }
        #endregion

        #region GetData
        public T? GetData<T>(string key)
        {
            String? data = _cache?.GetString(key);

            if (data is null)
                return default(T);

            return JsonSerializer.Deserialize<T>(data)!;
        }
        #endregion

        #region SetData
        public void SetData<T>(string key, T data)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            _cache?.SetString(key, JsonSerializer.Serialize(data), options);
        }
        #endregion

        #region RemoveData
        public bool? RemoveData<T>(string key)
        {
            String? data = _cache?.GetString(key);

            if (data is null)
                return false;

            _cache?.Remove(key);

            return true;
        }
        #endregion
    }
}
