using Floo.Core.Shared;

namespace Floo.App.Shared.Cms.Tags
{
    public class TagDto : BaseDto<long>
    {
        public string Name { get; set; }

        public string Alias { get; set; }

        public string Cover { get; set; }

        public string Description { get; set; }
    }
}