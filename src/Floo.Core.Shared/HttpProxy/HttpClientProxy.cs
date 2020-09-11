using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Floo.Core.Shared.HttpProxy.Extensions;
using Floo.Core.Shared.DynamicProxy;
using Microsoft.AspNetCore.Http;

namespace Floo.Core.Shared.HttpProxy
{
    public class HttpClientProxy : IInterceptor
    {
        private readonly HttpClient httpClient;

        private readonly IHttpContextAccessor httpContextAccessor;

        public string RequestHost { get; set; }

        public HttpClientProxy(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.RequestHost = httpClient.BaseAddress.ToString();
        }

        public HttpClientProxy(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClient = httpClient;
            this.RequestHost = httpClient.BaseAddress.ToString();
            this.httpContextAccessor = httpContextAccessor;
        }

        public object Intercept(MethodInfo method, object[] @params)
        {
            var httpContext = AspectContext.From(method);

            var actionContext = new ApiActionContext
            {
                HttpApiClient = this,
                RequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri(string.Concat(RequestHost.Trim('/'), "/", method.GetPath().Trim('/'))),
                },
                ApiActionDescriptor = httpContext.ApiActionDescriptor.Clone() as ApiActionDescriptor
            };

            if (httpContextAccessor?.HttpContext != null)
            {
                actionContext.RequestMessage.Headers.TryAddWithoutValidation("Authorization", httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault());
            }

            var parameters = actionContext.ApiActionDescriptor.Parameters;
            for (var i = 0; i < parameters.Length; i++)
            {
                parameters[i].Value = @params[i];
            }

            var apiAction = httpContext.ApiActionDescriptor;

            try
            {
                return apiAction.Execute(actionContext);
            }
            catch (Exception ex)
            {
                var errMsg = ex.Message;

                while (ex.InnerException != null)
                {
                    errMsg += "--->" + ex.InnerException.Message;
                    ex = ex.InnerException;
                }
                throw new Exception($"Remote request error: [{actionContext.RequestMessage?.Method.Method}]{actionContext.RequestMessage?.RequestUri.ToString() ?? RequestHost} ExceptionMessage:{errMsg}", ex);
            }
        }

        internal async Task SendAsync(ApiActionContext context)
        {
            context.ResponseMessage = await this.httpClient.SendAsync(context.RequestMessage);

            if (!context.ResponseMessage.IsSuccessStatusCode)
                throw new HttpRequestException(context.ResponseMessage.ReasonPhrase);
        }
    }
}