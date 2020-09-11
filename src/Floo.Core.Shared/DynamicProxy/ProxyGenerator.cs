using Floo.Core.Shared.Utils;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Floo.Core.Shared.DynamicProxy
{
    public class ProxyGenerator : DispatchProxy
    {
        private static readonly ConcurrentCache<Type, Func<object>> funcCache = new ConcurrentCache<Type, Func<object>>();

        private IInterceptor Interceptor { get; set; }

        public static object Create(Type targetType, IInterceptor interceptor)
        {
            var func = funcCache.GetOrAdd(targetType, _targetType =>
            {
                var variable = Expression.Variable(_targetType);
                var callexp = Expression.Call(typeof(DispatchProxy), nameof(DispatchProxy.Create), new[] { _targetType, typeof(ProxyGenerator) });
                var interceptorProperty = Expression.Property(Expression.Convert(variable, typeof(ProxyGenerator)), nameof(Interceptor));
                var assign1 = Expression.Assign(variable, callexp);
                var assign2 = Expression.Assign(interceptorProperty, Expression.Constant(interceptor));
                var block = Expression.Block(new[] { variable }, assign1, assign2, variable);
                return Expression.Lambda<Func<object>>(block).Compile();
            });

            return func();
        }

        public static TTarget Create<TTarget>(IInterceptor iInterceptor)
        {
            return DispatchProxyDelegate<TTarget>.GetFunc()(iInterceptor);
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            return this.Interceptor.Intercept(targetMethod, args);
        }

        internal class DispatchProxyDelegate<TTarget>
        {
            private static Func<IInterceptor, TTarget> dispatchProxyFunc;

            public static Func<IInterceptor, TTarget> GetFunc()
            {
                if (dispatchProxyFunc == null)
                {
                    var targetType = typeof(TTarget);
                    var interceptorType = typeof(IInterceptor);
                    var variable = Expression.Variable(targetType);
                    var parm = Expression.Parameter(interceptorType);
                    var callexp = Expression.Call(typeof(DispatchProxy), nameof(DispatchProxy.Create), new[] { targetType, typeof(ProxyGenerator) });
                    var interceptorProperty = Expression.Property(Expression.Convert(variable, typeof(ProxyGenerator)), nameof(Interceptor));

                    var assign1 = Expression.Assign(variable, callexp);
                    var assign2 = Expression.Assign(interceptorProperty, parm);

                    var block = Expression.Block(new[] { variable }, parm, assign1, assign2, variable);
                    dispatchProxyFunc = Expression.Lambda<Func<IInterceptor, TTarget>>(block, parm).Compile();
                }
                return dispatchProxyFunc;
            }
        }
    }
}