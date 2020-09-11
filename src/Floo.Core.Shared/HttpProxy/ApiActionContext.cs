using System.Net.Http;

namespace Floo.Core.Shared.HttpProxy
{
    internal class ApiActionContext
    {
        public HttpClientProxy HttpApiClient { get; set; }

        public ApiActionDescriptor ApiActionDescriptor { get; set; }

        public HttpRequestMessage RequestMessage { get; set; }

        public HttpResponseMessage ResponseMessage { get; set; }
    }
}