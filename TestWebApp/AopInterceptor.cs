using System;
using Castle.DynamicProxy;
using System.Diagnostics;
using NLog;

namespace TestWebApp
{
    public abstract class AopInterceptor : IInterceptor
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void Intercept(IInvocation invocation)
        {
            var sw = new Stopwatch();
            sw.Start();

            var methodName = $"{invocation.TargetType.FullName}.{invocation.Method.Name}";

            Logger.Info($"aop :{methodName} --->");

            Exception temp_ex = null;
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                temp_ex = ex;
                Logger.Error(ex);
            }
            sw.Stop();
            var elapsedMilliseconds = sw.ElapsedMilliseconds;
            Logger.Fatal($"aop :{methodName} <---耗时{elapsedMilliseconds}毫秒");

            if (temp_ex != null)
                throw temp_ex;
        }
    }
}