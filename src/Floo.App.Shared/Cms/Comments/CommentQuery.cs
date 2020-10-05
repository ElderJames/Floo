namespace Floo.App.Shared.Cms.Comments
{
    public class CommentQuery : BaseQuery
    {
        public CommentQuery()
        {
            OrderByDesc = new[] { nameof(BaseDtoWithDatetime<long>.CreatedAtUtc) };
        }

        public long ContentId { get; set; }
    }
}
