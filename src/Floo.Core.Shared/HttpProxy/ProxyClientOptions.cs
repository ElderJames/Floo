using System;
using System.Collections.Generic;
using System.Text;

namespace Floo.Core.Shared.HttpProxy
{
    public class ProxyClientOptions
    {
        public string RequestHost { get; set; }

        public string[] AssemblyString { get; set; }

        public TimeSpan[] RetrySleepDurations { get; set; }

        public Func<Type, bool> Filter { get; set; }

        public ProxyClientOptions()
        {
            this.Filter = type => type.IsInterface && type.Name.EndsWith("Service");
            this.RetrySleepDurations = new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10)
            };
        }
    }
}