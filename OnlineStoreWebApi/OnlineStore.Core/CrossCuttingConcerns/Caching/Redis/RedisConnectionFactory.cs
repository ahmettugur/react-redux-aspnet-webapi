using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.CrossCuttingConcerns.Caching.Redis
{
    public class RedisConnectionFactory
    {
        static RedisConnectionFactory()
        {
            lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                var url = ConfigurationManager.AppSettings["RedisCacheUrl"].ToString();
                return ConnectionMultiplexer.Connect(url);
            });
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection;

        public static ConnectionMultiplexer Connection => lazyConnection.Value;

        public static void DisposeConnection()
        {
            if (lazyConnection.Value.IsConnected)
                lazyConnection.Value.Dispose();
        }
    }
}
