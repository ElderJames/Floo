using Floo.Core.Shared;

namespace Floo.App.Shared.Cms.Channels
{
    public class ChannelDto : BaseDto<long>
    {
        public string Cover { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Description { get; set; }
    }
}