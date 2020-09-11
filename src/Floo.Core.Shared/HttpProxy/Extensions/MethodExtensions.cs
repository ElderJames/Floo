using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Floo.Core.Shared.HttpProxy.Extensions
{
    public static class MethodExtensions
    {
        public static string GetPath(this MethodInfo method, Type declaringType = null)
        {
            return Regex.Replace($"http-proxy/method/{(declaringType ?? method.DeclaringType).FullName}/{method.Name}/{string.Join("/", method.GetParameters().Select(x => (x.ParameterType.IsArray ? x.ParameterType.Name + "array" : x.ParameterType.Name) + "/" + x.Name))}".ToLower(), "[^a-z|0-9|-]", "/").Replace("//", "/");
        }
    }
}