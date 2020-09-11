using System;

namespace Floo.Core.Shared.HttpProxy.Attributes
{
    public class AuthorizeAttribute : Attribute
    {
        public AuthorizeAttribute()
        {
        }

        public AuthorizeAttribute(string policy)
        {
            this.Policy = policy;
        }

        public string? AuthenticationSchemes { get; set; }

        public string? Policy { get; set; }

        public string? Roles { get; set; }
    }
}