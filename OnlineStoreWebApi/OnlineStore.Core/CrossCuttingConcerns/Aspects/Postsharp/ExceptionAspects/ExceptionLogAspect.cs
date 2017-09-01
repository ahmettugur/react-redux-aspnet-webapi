using OnlineStore.Core.CrossCuttingConcerns.Logging.Log4Net;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using PostSharp.Extensibility;

namespace OnlineStore.Core.CrossCuttingConcerns.Aspects.Postsharp.ExceptionAspects
{
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Method, TargetExternalMemberAttributes = MulticastAttributes.Instance)]
    public class ExceptionLogAspect : OnExceptionAspect
    {
        private Type _loggerType;
        private LoggerService _loggerService;

        public ExceptionLogAspect(Type loggerType)
        {
            _loggerType = loggerType;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (_loggerType != null)
            {
                if (_loggerType.BaseType != typeof(LoggerService))
                {
                    throw new Exception("Wrong Logger Type");
                }

                _loggerService = (LoggerService)Activator.CreateInstance(_loggerType);
            }


            base.RuntimeInitialize(method);
        }

        public override void OnException(MethodExecutionArgs args)
        {
            if (!_loggerService.IsErrorEnabled)
            {
                return;
            }
            _loggerService.Error(args.Exception);
        }
    }
}
