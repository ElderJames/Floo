using Floo.Core.Shared;

namespace Floo.Core.Entities.Cms
{
    public class Comment : BaseEntity
    {
        public long? ReplyId { get; set; }

        public long? ArticleId { get; set; }

        public string Content { get; set; }
    }
}