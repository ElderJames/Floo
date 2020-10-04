namespace Floo.App.Shared.Cms.Answers
{
    public class AnswerQuery : BaseQuery
    {
        public AnswerQuery()
        {
            OrderByDesc = new[] { nameof(BaseDtoWithDatetime<long>.CreatedAtUtc) };
        }

        public long ContentId { get; set; }
    }
}
