using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Floo.Core.Shared.HttpProxy.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Floo.App.Server.ProxyServer.Internal
{
    internal class ActionModelConvention : IActionModelConvention
    {
        public ActionModelConvention(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        private Type serviceType { get; }

        public void Apply(ActionModel action)
        {
            if (!serviceType.IsAssignableFrom(action.Controller.ControllerType)) return;

            var actionParams = action.ActionMethod.GetParameters();

            var method = serviceType.GetMethods().FirstOrDefault(mth => action.ActionMethod.Name == mth.Name && !actionParams.Except(mth.GetParameters(), new ModelConventionHelper.ParameterInfoEqualityComparer()).Any());

            if (method == null) return;

            var attrs = method.GetCustomAttributes();
            var actionAttrs = new List<object>();

            if (actionParams.All(x => x.ParameterType.IsUriParameterType() || x.ParameterType.IsUriParameterTypeArray()))
                actionAttrs.Add(new HttpGetAttribute(method.GetPath()));
            else if (!attrs.Any(x => x is HttpMethodAttribute || x is RouteAttribute))
                actionAttrs.Add(new HttpPostAttribute(method.GetPath()));

            action.Selectors.Clear();
            ModelConventionHelper.AddRange(action.Selectors, ModelConventionHelper.CreateSelectors(actionAttrs));
        }
    }
}