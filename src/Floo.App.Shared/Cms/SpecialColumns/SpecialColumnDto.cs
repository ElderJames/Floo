using Floo.Core.Shared;

namespace Floo.App.Shared.Cms.SpecialColumns
{
    public class SpecialColumnDto : BaseDto<long>
    {
        public long? ReplyId { get; set; }

        public long? ArticleId { get; set; }

        public string Content { get; set; }
    }
}