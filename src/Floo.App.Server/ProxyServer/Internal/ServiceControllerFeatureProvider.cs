using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Floo.App.Server.ProxyServer.Internal
{
    internal class ServiceControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private const string ControllerTypeNameSuffix = "Controller";
        private readonly IEnumerable<Type> serviceTypes;

        public ServiceControllerFeatureProvider(IEnumerable<Type> ServiceTypes)
        {
            this.serviceTypes = ServiceTypes;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var type in TypeScanner.AllTypes)
            {
                if (IsController(type) || serviceTypes.Any(o => type.IsClass && o.IsAssignableFrom(type)) && !feature.Controllers.Contains(type))
                {
                    feature.Controllers.Add(type.GetTypeInfo());
                }
            }
        }

        protected bool IsController(Type typeInfo)
        {
            if (!typeInfo.IsClass)
            {
                return false;
            }

            if (typeInfo.IsAbstract)
            {
                return false;
            }

            if (!typeInfo.IsPublic)
            {
                return false;
            }

            if (typeInfo.ContainsGenericParameters)
            {
                return false;
            }

            if (typeInfo.IsDefined(typeof(NonControllerAttribute)))
            {
                return false;
            }

            if (!typeInfo.Name.EndsWith(ControllerTypeNameSuffix, StringComparison.OrdinalIgnoreCase) &&
                !typeInfo.IsDefined(typeof(ControllerAttribute)))
            {
                return false;
            }

            return true;
        }
    }
}