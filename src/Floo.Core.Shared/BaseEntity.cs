using System;

namespace Floo.Core.Shared
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }

        public long CreateBy { get; set; }

        public long UpdateBy { get; set; }

        public DateTimeOffset CreateAt { get; set; }

        public DateTimeOffset UpdateAt { get; set; }
    }
}