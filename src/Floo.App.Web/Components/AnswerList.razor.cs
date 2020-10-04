using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Floo.App.Shared.Cms.Answers;

namespace Floo.App.Web.Components
{
    public partial class AnswerList : ComponentBase
    {
        [Parameter]
        public List<AnswerDto> Answers { get; set; } = new List<AnswerDto>();

    }
}
