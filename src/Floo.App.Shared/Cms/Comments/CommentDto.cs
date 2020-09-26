
using System;

namespace Floo.App.Shared.Cms.Comments
{
    public class CommentDto : BaseDtoWithDatetime<long>
    {
        public long? ReplyId { get; set; }

        public long? ContentId { get; set; }

        public string Author { get; set; }
        public string Avatar { get; set; }

        public string Content { get; set; }
    }
}