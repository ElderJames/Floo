using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace Floo.App.Server.ProxyServer.Internal
{
    internal static class TypeScanner
    {
        public static IEnumerable<Type> AllTypes { get; }

        static TypeScanner()
        {
            string platform = Environment.OSVersion.Platform.ToString();
            IEnumerable<AssemblyName> runtimeAssemblyNames = DependencyContext.Default.GetRuntimeAssemblyNames(platform);
            AllTypes = runtimeAssemblyNames.Select(Assembly.Load).SelectMany(a => a.ExportedTypes);
        }
    }
}