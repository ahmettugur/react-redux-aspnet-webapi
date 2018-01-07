using OnlineStore.Core.CrossCuttingConcerns.Caching;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using OnlineStore.Core.Contracts.Entities;
using System.Dynamic;
//using OnlineStore.Entity.Concrete;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace OnlineStore.Core.CrossCuttingConcerns.Aspects.Postsharp.CacheAsepcts
{
    [Serializable]
    public class CacheAspect : MethodInterceptionAspect
    {
        private Type _caheType;
        private Type _returnType;
        private int _expireAsMinute;
        private ICacheManager _cacheManager;


        /// <summary>
        /// If you want to use redis cache returnType is cannot be null
        /// </summary>
        /// <param name="caheType"></param>
        /// <param name="expireAsMinute"></param>
        /// <param name="returnType"></param>
        public CacheAspect(Type caheType, int expireAsMinute = 60, Type returnType = null)
        {
            _caheType = caheType;
            _expireAsMinute = expireAsMinute;
            _returnType = returnType;
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

        public object ConvertExpandoObjectToEntity(ExpandoObject source, Type e)
        {
            IEntity example = (IEntity)Activator.CreateInstance(e);

            IDictionary<string, object> dict = source;

            PropertyInfo[] props = example.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (dict.Keys.Any(_=>_ == prop.Name))
                {
                    object valueStr = dict[prop.Name].ToString();
                    dynamic value = valueStr;
                    var converter = TypeDescriptor.GetConverter(prop.PropertyType);
                    var result = converter.ConvertFrom(value);

                    example.GetType().GetProperty(prop.Name).SetValue(example, result, null);
                }
            }
            return example;
        }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var methodName = $"{args.Method.ReflectedType.Namespace}.{args.Method.ReflectedType.Name}.{args.Method.Name}";
            var arguments = args.Arguments.ToList();
            var key = $"{methodName}.{string.Join(",", arguments.Select(_ => _ != null ? _.ToString() : "<Nullable>"))}";

            if (_cacheManager.IsExist(key))
            {
                if (_returnType != null)
                {
                    Type genericListType = typeof(List<>);
                    Type customListType = genericListType.MakeGenericType(_returnType);
                    IList customListInstance = (IList)Activator.CreateInstance(customListType);

                    List<ExpandoObject> list = _cacheManager.Get<List<ExpandoObject>>(key);
                    foreach (var item in list)
                    {
                        object c = ConvertExpandoObjectToEntity(item, _returnType);
                        customListInstance.Add(c);
                    }
                    args.ReturnValue = customListInstance;
                }
                else
                {
                    args.ReturnValue = _cacheManager.Get<object>(key);
                }
                return;
            }

            base.OnInvoke(args);

            _cacheManager.Add(key, args.ReturnValue, _expireAsMinute);
        }
    }
}
