using Floo.App.Shared.Cms.Contents;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Floo.App.Web.Components
{
    public partial class ContentList: ComponentBase
    {
        [Parameter]
        public List<ContentDto> Contents { get; set; } = new List<ContentDto>();

    }
}
