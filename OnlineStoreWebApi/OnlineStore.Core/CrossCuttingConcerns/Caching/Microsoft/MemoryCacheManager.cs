using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OnlineStore.Core.CrossCuttingConcerns.Caching.Microsoft
{
    [Serializable]
    public class MemoryCacheManager : ICacheManager
    {
        private readonly MemoryCache _cache = MemoryCache.Default;

        public void Add(string key, object data, int expireAsMinute)
        {
            if (data == null)
            {
                return;
            }

            if (IsExist(key))
            {
                Remove(key);
            }

            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(expireAsMinute) };
            _cache.Add(new CacheItem(key, data), policy);
        }

        public void Clear()
        {
            foreach (var item in _cache)
            {
                Remove(item.Key);
            }
        }

        public T Get<T>(string key)
        {
            return (T)_cache[key];
        }

        public bool IsExist(string key)
        {
            return _cache.Any(_ => _.Key == key);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        //public void RemoveByPattern(string pattern)
        //{
        //    var regexp = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        //    var cacheToRemove = _cache.Where(_ => regexp.IsMatch(_.Key)).Select(_ => _.Key).ToList();

        //    foreach (var key in cacheToRemove)
        //    {
        //        Remove(key);
        //    }
        //}
    }
}
