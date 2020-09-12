using Floo.Core.Shared;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Floo.Infrastructure
{
    public class IdentityContext : IIdentityContext
    {
        public static readonly IdentityContext Empty = new IdentityContext();

        public IdentityContext()
        {
        }

        public IdentityContext(long? userId, string userName)
        {
            this.UserId = userId;
            this.UserName = UserName;
        }

        public IdentityContext(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            if (user != null)
            {
                this.UserId = this.GetClaimValueAsLong(user, JwtClaimTypes.Subject);
                this.UserName = this.GetClaimValue(user, JwtClaimTypes.Name);
            }
        }

        public virtual long? UserId { get; private set; }

        public virtual string UserName { get; private set; }

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