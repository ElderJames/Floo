namespace Floo.App.Shared.Cms.Articles
{
    public class ArticleDetailDto : BaseDtoWithDatetime<long>
    {
        public string Title { get; set; }

        public string Slug { get; set; }

        public string Summary { get; set; }

        public string Cover { get; set; }

        public string Author { get; set; }

        public string ChannelName { get; set; }

        public string Content { get; set; }
        public long ContentId { get; set; }
    }
}
