namespace Floo.App.Shared.Cms.Questions
{
    public class QuestionDto
    {
        public long? Id { get; set; }
        public string Title { get; set; }

        public string Slug { get; set; }

        public string Summary { get; set; }

        public string Cover { get; set; }

        public string Source { get; set; }
    }
}