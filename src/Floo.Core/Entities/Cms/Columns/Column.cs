using Floo.Core.Shared;

namespace Floo.Core.Entities.Cms.SpecialColumns
{
    public class Column : BaseEntity
    {
        public string Cover { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Description { get; set; }
    }
}