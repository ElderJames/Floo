
namespace Floo.App.Shared.Cms.Comments
{
    public class CommentDto : BaseDto<long>
    {
        public long? ReplyId { get; set; }

        public long? ContentId { get; set; }

        public string Content { get; set; }
    }
}