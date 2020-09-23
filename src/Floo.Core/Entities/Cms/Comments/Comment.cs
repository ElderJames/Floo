using Floo.Core.Entities.Cms.Contents;
using Floo.Core.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace Floo.Core.Entities.Cms.Comments
{
    public class Comment : BaseEntity
    {
        public long? CommentId { get; set; }

        public long? ContentId { get; set; }

        public string Text { get; set; }

        public Content Content { get; set; }

        [ForeignKey(nameof(CommentId))]
        public Comment Parent { get; set; }
    }
}