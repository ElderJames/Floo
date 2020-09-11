using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Floo.Core.Shared.HttpProxy.Extensions;
using Floo.Core.Shared.Utils;

namespace Floo.Core.Shared.HttpProxy
{
    internal class AspectContext
    {
        public ApiActionDescriptor ApiActionDescriptor { get; private set; }

        private static readonly ConcurrentCache<MethodInfo, AspectContext> cache;

        static AspectContext()
        {
            cache = new ConcurrentCache<MethodInfo, AspectContext>(new IInvocationComparer());
        }

        public static AspectContext From(MethodInfo method)
        {
            return cache.GetOrAdd(method, GetContextNoCache(method));
        }

        private static AspectContext GetContextNoCache(MethodInfo method)
        {
            return new AspectContext
            {
                ApiActionDescriptor = GetActionDescriptor(method)
            };
        }

        private static ApiActionDescriptor GetActionDescriptor(MethodInfo method)
        {
            var descriptor = new ApiActionDescriptor
            {
                Name = method.Name,
                ReturnTaskType = method.ReturnType,
                ReturnDataType = method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition().IsAssignableFrom(typeof(Task<>)) ? method.ReturnType.GetGenericArguments().FirstOrDefault() : method.ReturnType,
                Parameters = method.GetParameters().Select((param, index) => GetParameterDescriptor(param, index, method)).ToArray()
            };

            return descriptor;
        }

        private static ApiParameterDescriptor GetParameterDescriptor(ParameterInfo parameter, int index, MethodInfo method)
        {
            var parameterDescriptor = new ApiParameterDescriptor
            {
                Name = parameter.Name,
                Index = index,
                ParameterType = parameter.ParameterType,
                IsUriParameterType = parameter.ParameterType.IsUriParameterType(),
            };

            return parameterDescriptor;
        }

        private class IInvocationComparer : IEqualityComparer<MethodInfo>
        {
            public bool Equals(MethodInfo x, MethodInfo y)
            {
                if (x == null && y == null)
                    return true;

                if (x == null || y == null)
                    return false;

                return x.Name == y.Name && x.DeclaringType?.FullName == y.DeclaringType?.FullName;
            }

            public int GetHashCode(MethodInfo obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}