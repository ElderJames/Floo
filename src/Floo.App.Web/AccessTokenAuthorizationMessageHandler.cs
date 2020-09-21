using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.App.Web
{
    public class AccessTokenAuthorizationMessageHandler : DelegatingHandler
    {
        private readonly IAccessTokenProvider _provider;
        private AccessToken _lastToken;
        private AuthenticationHeaderValue _cachedHeader;
        private AccessTokenRequestOptions _tokenOptions;

        public AccessTokenAuthorizationMessageHandler(
            IAccessTokenProvider provider)
        {
            _provider = provider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var now = DateTimeOffset.Now;

            if (_lastToken == null || now >= _lastToken.Expires.AddMinutes(-5))
            {
                var tokenResult = _tokenOptions != null ?
                    await _provider.RequestAccessToken(_tokenOptions) :
                    await _provider.RequestAccessToken();

                if (tokenResult.TryGetToken(out var token))
                {
                    _lastToken = token;
                    _cachedHeader = new AuthenticationHeaderValue("Bearer", _lastToken.Value);
                }
            }

            request.Headers.Authorization = _cachedHeader;

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
