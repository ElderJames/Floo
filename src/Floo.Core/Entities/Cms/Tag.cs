using Floo.Core.Shared;

namespace Floo.Core.Entities.Cms
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }

        public string Alias { get; set; }

        public string Cover { get; set; }

        public string Description { get; set; }
    }
}