using Floo.App.Shared.Cms.Channels;
using Floo.App.Shared.Cms.Contents;
using Floo.App.Shared.Cms.SpecialColumns;

namespace Floo.App.Shared.Cms.Articles
{
    public class ArticleDto : BaseDtoWithDatetime<long>
    {
        public string Title { get; set; }

        public string Slug { get; set; }

        public string Summary { get; set; }

        public string Cover { get; set; }

        public string Source { get; set; }

        public string Author { get; set; }

        public long? ChannelId { get; set; }

        public long? SpecialColumnId { get; set; }

        public ContentDto Contnet { get; set; }

        public ChannelDto Channel { get; set; }

        public ColumnDto Column { get; set; }
    }
}