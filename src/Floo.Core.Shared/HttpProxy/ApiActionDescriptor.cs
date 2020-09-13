using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Floo.Core.Shared.HttpProxy.Extensions;

namespace Floo.Core.Shared.HttpProxy
{
    internal class ApiActionDescriptor
    {
        public string Name { get; set; }

        public ApiParameterDescriptor[] Parameters { get; set; }

        public Type ReturnTaskType { get; set; }

        public Type ReturnDataType { get; set; }

        private JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true
        };

        public object Execute(ApiActionContext context)
        {
            if (ReturnTaskType.IsGenericType && ReturnTaskType.GetGenericTypeDefinition() == typeof(Task<>))
                return this.ExecuteAsync(context).CastResult(this.ReturnDataType);
            else
                return Task.Run(() => this.ExecuteAsync(context)).Result;
        }

        private async Task<object> ExecuteAsync(ApiActionContext context)
        {
            foreach (var parameter in context.ApiActionDescriptor.Parameters)
            {
                if (parameter.Value is CancellationToken)
                {
                    continue;
                }

                if (parameter.IsUriParameterType || parameter.ParameterType.IsUriParameterTypeArray())
                {
                    var uri = context.RequestMessage.RequestUri;
                    var pathQuery = GetPathQuery(uri, parameter);
                    context.RequestMessage.RequestUri = new Uri(uri, pathQuery);
                    context.RequestMessage.Method = HttpMethod.Get;
                }
                else
                {
                    var jsonStr = parameter.Value == null ? null : JsonSerializer.Serialize(parameter.Value, jsonOptions);
                    context.RequestMessage.Content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                    context.RequestMessage.Method = HttpMethod.Post;
                }
            }

            await context.HttpApiClient.SendAsync(context);
            var response = context.ResponseMessage.EnsureSuccessStatusCode();
            var responseStr = await response.Content.ReadAsStringAsync();
            var dataType = context.ApiActionDescriptor.ReturnDataType;

            if (context.ResponseMessage.Content.Headers.ContentType.MediaType == "application/json")
            {
                return JsonSerializer.Deserialize(responseStr, dataType, jsonOptions);
            }

            if (dataType.IsUriParameterType())
            {
                return Convert.ChangeType(responseStr, dataType);
            }

            return null;
        }

        public object Clone()
        {
            return new ApiActionDescriptor
            {
                Name = this.Name,
                ReturnDataType = this.ReturnDataType,
                ReturnTaskType = this.ReturnTaskType,
                Parameters = this.Parameters.Select(item => (ApiParameterDescriptor)item.Clone()).ToArray()
            };
        }

        private string GetPathQuery(Uri uri, ApiParameterDescriptor parameter)
        {
            var _params = new List<(string, string)>();

            var template = uri.LocalPath.Trim('/');
            var queryString = HttpUtility.UrlDecode(uri.Query).TrimStart('?');

            if (!string.IsNullOrEmpty(queryString))
            {
                var keyValues = queryString.Split('&').Select(group =>
                {
                    var keyValue = group.Split('=');
                    return new { Key = keyValue[0], Value = keyValue[1] };
                });

                foreach (var kv in keyValues)
                {
                    _params.Add((kv.Key, kv.Value.ToString()));
                }
            }
            if (parameter.ParameterType.IsArray && parameter.Value is Array array)
            {
                foreach (var item in array)
                {
                    if (item.GetType().IsEnum)
                    {
                        Type underlyingType = Enum.GetUnderlyingType(item.GetType());
                        _params.Add((parameter.Name, Convert.ChangeType(item, underlyingType).ToString()));
                    }
                    else
                    {
                        _params.Add((parameter.Name, item.ToString()));
                    }
                }
            }
            else if (parameter.ParameterType.IsEnum)
            {
                Type underlyingType = Enum.GetUnderlyingType(parameter.ParameterType);
                _params.Add((parameter.Name, Convert.ChangeType(parameter.Value, underlyingType).ToString()));
            }
            else if (parameter.IsUriParameterType)
            {
                _params.Add((parameter.Name, string.Format(CultureInfo.InvariantCulture, "{0}", parameter.Value)));
            }
            else
            {
                var instance = parameter.Value;
                var instanceType = parameter.ParameterType;

                var properties = Property.GetProperties(instanceType);
                foreach (var p in properties)
                {
                    var value = instance == null ? null : p.GetValue(instance);
                    _params.Add((p.Name, string.Format(CultureInfo.InvariantCulture, "{0}", value)));
                }
            }

            return BoundTemplate(template, _params);
        }

        public string BoundTemplate(string path, List<(string, string)> keyValues)
        {
            return "?" + string.Join("&", keyValues.Select(x => $"{x.Item1}={WebUtility.UrlEncode(x.Item2)}"));
        }
    }
}