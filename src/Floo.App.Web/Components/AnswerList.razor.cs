using Floo.App.Shared.Cms.Comments;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Floo.App.Web.Components
{
    public partial class AnswerList : ComponentBase
    {
        [Parameter]
        public List<CommentDto> Answers { get; set; } = new List<CommentDto>();

    }
}
