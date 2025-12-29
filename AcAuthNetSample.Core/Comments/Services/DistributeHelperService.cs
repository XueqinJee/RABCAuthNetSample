using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Comments.Services {

    /// <summary>
    /// 分布式缓存帮助类
    /// </summary>
    public class DistributeHelperService {
        private readonly ILogger<DistributeHelperService> _logger;
        private readonly IDistributedCache _distributedCache;

        private readonly TimeSpan DefaultTimeSpan = TimeSpan.FromSeconds(5);

        public DistributeHelperService(IDistributedCache distributedCache, ILogger<DistributeHelperService> logger)
        {
            _distributedCache = distributedCache;
            _logger = logger;
        }

        public T? Get<T>(string key) where T : class
        {

            var value = _distributedCache.GetString(key);
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<T>(value);
        }

        public void Set(string key, object? data, TimeSpan? timeSpan = null)
        {
            if (timeSpan == null)
            {
                timeSpan = DefaultTimeSpan;
            }

            _distributedCache.SetString(key, JsonConvert.SerializeObject(data), new DistributedCacheEntryOptions
            {
                SlidingExpiration = timeSpan,
            });
        }

        public async Task SetAsync(string key, object? data, TimeSpan? timeSpan = null)
        {
            if (timeSpan == null)
            {
                timeSpan = DefaultTimeSpan;
            }
            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(data), new DistributedCacheEntryOptions
            {
                SlidingExpiration = timeSpan
            });
        }


        public void Remove(string key)
        {
            _distributedCache.Remove(key);
        }
    }
}
