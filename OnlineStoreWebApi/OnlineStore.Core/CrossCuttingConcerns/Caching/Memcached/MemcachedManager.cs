using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.CrossCuttingConcerns.Caching.Memcached
{
    public class MemcachedManager : ICacheManager
    {
        private MemcachedClientConfiguration config;
        private MemcachedClient client;

        public MemcachedManager()
        {
            string server = "127.0.0.1";
            int port = 11211;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MemcachedUrl"]) && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["MemcachedPort"]))
            {
                server = ConfigurationManager.AppSettings["MemcachedUrl"];
                port = Convert.ToInt32(ConfigurationManager.AppSettings["MemcachedPort"]);
            }

            config = new MemcachedClientConfiguration();
            config.AddServer(server, port);
            config.Protocol = MemcachedProtocol.Binary;
            client = new MemcachedClient(config);
        }

        public void Add(string key, object data, int expireAsMinute)
        {
            var newDate = DateTime.Now.AddMinutes(expireAsMinute);
            var expireTimeSpan = newDate.Subtract(DateTime.Now);
            client.Store(StoreMode.Set, key, JsonConvert.SerializeObject(data), expireTimeSpan);

            var t = client.Get(key);

            if (t != null)
            {

            }
        }

        public void Clear()
        {
            client.FlushAll();
        }

        public T Get<T>(string key)
        {
            var cacheObject = client.Get(key);
            var val = JsonConvert.DeserializeObject<T>(cacheObject.ToString());
            return val;
        }

        public bool IsExist(string key)
        {
            return client.Get(key) != null;
        }

        public void Remove(string key)
        {
            client.Remove(key);
        }

        //public void RemoveByPattern(string pattern)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
