﻿using Floo.Core.Shared;
using IdentityModel;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Floo.App.Server
{
    public class IdentityContext : IIdentityContext
    {
        AuthenticationStateProvider _authenticationStateProvider;
        ClaimsPrincipal _claimsPrincipal;
        private long? userId;
        private string userName;

        public IdentityContext(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _authenticationStateProvider.AuthenticationStateChanged += async (t) => await GetState();
        }

        public async Task GetState()
        {
            var userState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            _claimsPrincipal = userState.User;
        }

        public virtual long? UserId=> this.GetClaimValueAsLong(_claimsPrincipal, JwtClaimTypes.Subject);

        public virtual string UserName=> this.GetClaimValue(_claimsPrincipal, JwtClaimTypes.Name);

        public string NickName => this.GetClaimValue(_claimsPrincipal, JwtClaimTypes.NickName);

        public string Avatar => this.GetClaimValue(_claimsPrincipal, JwtClaimTypes.Picture);

        private string GetClaimValue(ClaimsPrincipal user, string claimType)
        {
            var first = user?.FindFirst(claimType);
            return first?.Value;
        }

        private long? GetClaimValueAsLong(ClaimsPrincipal user, string claimType)
        {
            var claimValue = this.GetClaimValue(user, claimType);
            if (claimValue == null)
            {
                return null;
            }

            if (!long.TryParse(claimValue, out var value))
            {
                return null;
            }

            return value;
        }
    }
}
