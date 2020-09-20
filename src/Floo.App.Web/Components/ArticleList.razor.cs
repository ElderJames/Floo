using AntDesign;
using Floo.App.Shared.Cms.Articles;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Floo.App.Web.Components
{
    public partial class ArticleList: ComponentBase
    {
        [Parameter]
        public List<ArticleDto> Articles { get; set; } = new List<ArticleDto>();

    }
}
