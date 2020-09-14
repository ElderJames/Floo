using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using FlooAuthorizeAttribute = Floo.Core.Shared.HttpProxy.Attributes.AuthorizeAttribute;
using MvcAuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace Floo.App.Server.ProxyServer.Internal
{
    internal class ControllerModelConvention : IControllerModelConvention
    {
        public ControllerModelConvention(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        private Type serviceType { get; }

        public void Apply(ControllerModel controller)
        {
            if (!serviceType.IsAssignableFrom(controller.ControllerType)) return;

            var attrs = serviceType.GetCustomAttributes();
            var controllerAttrs = new List<object>();

            foreach (var att in attrs)
            {
                if (att is RouteAttribute routeAttr)
                {
                    var template = routeAttr.Template;
                    controllerAttrs.Add(new RouteAttribute(template));
                }

                if (att is FlooAuthorizeAttribute authorize)
                {
                    controller.Filters.Add(new AuthorizeFilter(new[] { new MvcAuthorizeAttribute() {
                        AuthenticationSchemes = authorize.AuthenticationSchemes,
                        Policy = authorize.Policy,
                        Roles = authorize.Roles
                    }}));
                }
            }

            if (controllerAttrs.Any())
            {
                controller.Selectors.Clear();
                ModelConventionHelper.AddRange(controller.Selectors, ModelConventionHelper.CreateSelectors(controllerAttrs));
            }
        }
    }
}