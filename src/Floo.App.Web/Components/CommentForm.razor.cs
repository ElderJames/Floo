using Floo.App.Shared.Cms.Comments;
using Microsoft.AspNetCore.Components;

namespace Floo.App.Web.Components
{
    public partial class CommentForm : ComponentBase
    {
        [Parameter]
        public CommentDto Comment { get; set; } =new CommentDto();
    }
}
