using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace OnlineStore.Core.CrossCuttingConcerns.Aspects.Postsharp.PerformanceAspects
{
    [Serializable]
    public class MethodWorkingTimeAspect : OnMethodBoundaryAspect
    {
        [NonSerialized]
        private Stopwatch _stopwatch;
        private int _interval;

        public MethodWorkingTimeAspect(int interval = 2)
        {
            _interval = interval;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            _stopwatch = Activator.CreateInstance<Stopwatch>();
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            _stopwatch.Start();

            base.OnEntry(args);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            //if (_stopwatch.Elapsed.TotalSeconds > _interval)
            //{
            Debug.WriteLine($"Performance: {args.Method.DeclaringType.FullName}.{args.Method.Name}  Expected -->> {_interval} --- Actual -->>{_stopwatch.Elapsed.TotalSeconds}");
            //}
            _stopwatch.Stop();
            base.OnExit(args);
        }

    }
}
