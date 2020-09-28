using Floo.Core.Entities.Cms.Contents;
using Floo.Core.Entities.Cms.Questions;
using Floo.Core.Shared;

namespace Floo.Core.Entities.Cms.Answers
{
    public class Answer : BaseEntity
    {
        public long ContentId { get; set; }

        public long QuestionId { get; set; }

        public Question Question { get; set; }

        public Content Content { get; set; }
    }
}
