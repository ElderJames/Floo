using Floo.Core.Shared;
using Microsoft.AspNetCore.Identity;
using System;

namespace Floo.Core.Entities.Identity
{
    public class User : IdentityUser<long>, IEntity<long>
    {
        public long CreatedBy { get; set; }

        public long UpdatedBy { get; set; }

        public bool Deleted { get; set; }

        public DateTime? CreatedAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }
    }
}