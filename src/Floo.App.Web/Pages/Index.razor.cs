using Floo.App.Shared.Cms.Articles;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Floo.App.Web.Pages
{
    public partial class Index
    {
        [Parameter]
        public string ChannelName { get; set; }

        [Inject] public IArticleService ArticleService { get; set; }

        private List<ArticleDto> _articleList = new List<ArticleDto>();

        private int _pageIndex = 1;

        private ArticleQuery articleQuery = new ArticleQuery();

        protected override async Task OnInitializedAsync()
        {
            await GetArticles();
        }

        private async Task GetArticles()
        {
            var articleResult = await ArticleService.QueryListAsync(articleQuery);
            if (articleResult.Items.Any())
            {
                _articleList.AddRange(articleResult.Items);
            }
        }
    }
}