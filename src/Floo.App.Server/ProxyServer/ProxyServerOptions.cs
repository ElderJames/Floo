using System;
using System.Collections.Generic;
using System.Text;

namespace Floo.App.Server.ProxyServer
{
    public class ProxyServerOptions
    {
        public string AssemblyString { get; set; }

        public Func<Type, bool> Filter { get; set; }

        public ProxyServerOptions()
        {
            this.Filter = type => type.IsInterface && type.Name.EndsWith("Service");
        }
    }
}