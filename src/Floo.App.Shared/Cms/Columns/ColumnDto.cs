using Floo.Core.Shared;

namespace Floo.App.Shared.Cms.SpecialColumns
{
    public class ColumnDto : BaseDto<long>
    {
        public long? ReplyId { get; set; }

        public long? ContentId { get; set; }

        public string Content { get; set; }
    }
}