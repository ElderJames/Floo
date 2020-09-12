using System;

namespace Floo.Core.Shared
{
    public interface IEntity
    {
        public long CreatedBy { get; set; }

        public long UpdatedBy { get; set; }

        public bool Deleted { get; set; }

        public DateTimeOffset CreatedAtUtc { get; set; }

        public DateTimeOffset UpdatedAtUtc { get; set; }
    }

    public interface IEntity<TKey> : IEntity
    {
        public TKey Id { get; set; }
    }
}