﻿using System;

namespace Floo.Core.Shared
{
    public abstract class BaseEntity : IEntity<long>
    {
        public long Id { get; set; }

        public long CreatedBy { get; set; }

        public long UpdatedBy { get; set; }

        public bool Deleted { get; set; }

        public DateTimeOffset CreatedAtUtc { get; set; }

        public DateTimeOffset UpdatedAtUtc { get; set; }
    }
}