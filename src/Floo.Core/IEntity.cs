using System;

namespace Floo.Core
{
    public interface IEntity
    {
        public long CreatedBy { get; set; }

        public long UpdatedBy { get; set; }

        public bool Deleted { get; set; }

        public DateTime? CreatedAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }
    }

    public interface IEntity<TKey> : IEntity
    {
        public TKey Id { get; set; }
    }
}