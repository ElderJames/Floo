using Floo.Core.Entities.Cms.Channels;
using Floo.Core.Entities.Cms.Contents;
using Floo.Core.Entities.Cms.SpecialColumns;
using Floo.Core.Shared;

namespace Floo.Core.Entities.Cms.Articles
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }

        public string Slug { get; set; }

        public string Summary { get; set; }
      
        public string Cover { get; set; }

        public string Source { get; set; }

        public long ChannelId { get; set; }

        public long ContnetId { get; set; }

        public Channel Channel { get; set; }

        public Column Column { get; set; }

        public Content Contnet { get; set; }
    }
}