using OnlineStore.Core.CrossCuttingConcerns.Caching;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace OnlineStore.Core.CrossCuttingConcerns.Aspects.Postsharp.CacheAsepcts
{
    [Serializable]
    public class CacheAspect : MethodInterceptionAspect
    {
        private Type _caheType;
        private int _expireAsMinute;
        private ICacheManager _cacheManager;

        public CacheAspect(Type caheType, int expireAsMinute = 60)
        {
            _caheType = caheType;
            _expireAsMinute = expireAsMinute;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (!typeof(ICacheManager).IsAssignableFrom(_caheType))
            {
                throw new Exception("Wrong Cache Manager.");
            }

            _cacheManager = (ICacheManager)Activator.CreateInstance(_caheType);

            base.RuntimeInitialize(method);
        }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var methodName = $"{args.Method.ReflectedType.Namespace}.{args.Method.ReflectedType.Name}.{args.Method.Name}";
            var arguments = args.Arguments.ToList();
            var key = $"{methodName}.{string.Join(",", arguments.Select(_ => _ != null ? _.ToString() : "<Nullable>"))}";

            if (_cacheManager.IsExist(key))
            {
                args.ReturnValue = _cacheManager.Get<object>(key);
            }

            base.OnInvoke(args);

            _cacheManager.Add(key, args.ReturnValue, _expireAsMinute);
        }
    }
}
