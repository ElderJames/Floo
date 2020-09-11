using System;
using System.Linq;
using System.Reflection;
using Floo.Core.Shared.HttpProxy.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Floo.App.Server.ProxyServer.Internal
{
    internal class ParameterModelConvention : IParameterModelConvention
    {
        public ParameterModelConvention(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        private Type serviceType { get; }

        public void Apply(ParameterModel parameter)
        {
            if (!serviceType.IsAssignableFrom(parameter.Action.Controller.ControllerType)) return;

            var actionParams = parameter.Action.ActionMethod.GetParameters();

            var method = serviceType.GetMethods().FirstOrDefault(mth => parameter.Action.ActionMethod.Name == mth.Name && !actionParams.Except(mth.GetParameters(), new ModelConventionHelper.ParameterInfoEqualityComparer()).Any());

            if (method == null) return;

            var theParam = parameter.ParameterInfo;
            var isGetMethod = method.GetCustomAttribute<HttpGetAttribute>(true) != null;

            if (theParam == null) return;

            var paramAttrs = new IBindingSourceMetadata[] { };

            if (theParam.ParameterType.IsUriParameterType() || theParam.ParameterType.IsUriParameterTypeArray())
                return;

            if (isGetMethod)
            {
                paramAttrs = new[] { new FromQueryAttribute() };
            }
            else
            {
                paramAttrs = new IBindingSourceMetadata[] { new FromBodyAttribute(), new FromFormAttribute() };
            }

            parameter.BindingInfo = BindingInfo.GetBindingInfo(paramAttrs);
        }
    }
}