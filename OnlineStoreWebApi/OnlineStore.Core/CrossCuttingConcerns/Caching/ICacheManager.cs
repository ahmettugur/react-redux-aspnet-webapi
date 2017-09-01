using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        void Add(string key, object data, int expireAsMinute);
        void Remove(string key);
        void RemoveByPattern(string pattern);
        bool IsExist(string key);
        void Clear();
    }
}
