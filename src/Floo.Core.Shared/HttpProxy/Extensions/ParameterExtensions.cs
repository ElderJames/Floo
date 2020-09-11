using Floo.Core.Shared.Utils;
using System;
using System.Linq;

namespace Floo.Core.Shared.HttpProxy.Extensions
{
    public static class ParameterExtensions
    {
        private static readonly ConcurrentCache<Type, bool> cache;

        private static readonly ConcurrentCache<Type, bool> arrCache;

        static ParameterExtensions()
        {
            cache = new ConcurrentCache<Type, bool>();
            arrCache = new ConcurrentCache<Type, bool>();
        }

        public static bool IsUriParameterType(this Type parameterType)
        {
            return cache.GetOrAdd(parameterType, type =>
            {
                if (type == null)
                    return false;

                if (type.IsGenericType)
                {
                    type = type.GetGenericArguments().FirstOrDefault();
                }

                if (type.IsPrimitive || parameterType.IsEnum)
                {
                    return true;
                }

                return type == typeof(string)
                       || type == typeof(decimal)
                       || type == typeof(DateTime)
                       || type == typeof(Guid)
                       || type == typeof(Uri);
            });
        }

        public static bool IsUriParameterTypeArray(this Type parameterType)
        {
            return arrCache.GetOrAdd(parameterType, type => type.BaseType == typeof(Array) &&
                                                         type.GetElementType().IsUriParameterType());
        }
    }
}