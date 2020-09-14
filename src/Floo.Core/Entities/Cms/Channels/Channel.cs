using Floo.Core.Shared;

namespace Floo.Core.Entities.Cms.Channels
{
    public class Channel : BaseEntity
    {
        public string Cover { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Description { get; set; }
    }
}