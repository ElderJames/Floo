using Floo.App.Shared.Cms.Comments;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Floo.App.Web.Components
{
    public partial class CommentList : ComponentBase
    {
        [Parameter]
        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();

    }
}
