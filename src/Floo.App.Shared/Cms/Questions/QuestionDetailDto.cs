namespace Floo.App.Shared.Cms.Questions
{
    public class QuestionDetailDto : BaseDtoWithDatetime<long>
    {
        public string Title { get; set; }

        public string Slug { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }
        public long ContentId { get; set; }
    }
}
