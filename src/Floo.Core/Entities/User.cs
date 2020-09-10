using Floo.Core.Shared;
using System;

namespace Floo.Core.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }

        public string Avatar { get; set; }

        public string Email { get; set; }

        public string SecurityStamp { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; }

        public string NormalizedUserName { get; set; }

        public string NormalizedEmail { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public bool LockoutEnabled { get; set; }

        public bool EmailConfirmed { get; set; }

        public string ConcurrencyStamp { get; set; }

        public int AccessFailedCount { get; set; }

        public bool TwoFactorEnabled { get; set; }
    }
}