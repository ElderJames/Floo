using Floo.Core.Shared;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace Floo.Infrastructure
{
    public class IdentityContext : IIdentityContext
    {
        public static readonly IdentityContext Empty = new IdentityContext();
        private long? userId;
        private string userName;

        private IPrincipal User { get; }

        public IdentityContext()
        {
        }

        public IdentityContext(long? userId, string userName)
        {
            this.userId = userId;
            this.userName = userName;
        }

        public IdentityContext(IHttpContextAccessor httpContextAccessor)
        {
            User = httpContextAccessor.HttpContext?.User;
        }

        public virtual long? UserId
        {
            get
            {
                if (!userId.HasValue && User != null && User is ClaimsPrincipal user)
                {
                    userId = this.GetClaimValueAsLong(user, ClaimTypes.NameIdentifier);
                }
                return userId;
            }
        }

        public virtual string UserName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(userName) && User != null && User is ClaimsPrincipal user)
                {
                    userName = this.GetClaimValue(user, JwtClaimTypes.Name);
                }
                return userName;
            }
        }

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