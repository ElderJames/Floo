using Floo.App.Shared.Cms.Articles;
using Floo.App.Web.Utils;
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

        [Inject] public PreFetchedState PreFetchedState { get; set; }

        private List<ArticleDto> _articleList;

        private int _pageIndex = 1;

        private ArticleQuery articleQuery = new ArticleQuery();

        protected override async Task OnInitializedAsync()
        {
            await GetArticles();
        }

        private async Task GetArticles()
        {
            var key = "home-articleList";
            if (_articleList == null)
            {
                _articleList = await PreFetchedState.GetAsync<List<ArticleDto>>(key);
            }
            if (_articleList == null)
            {
                _articleList = new List<ArticleDto>();
                var articleResult = await ArticleService.QueryListAsync(articleQuery);
                if (articleResult.Items.Any())
                {
                    _articleList.AddRange(articleResult.Items);

                    PreFetchedState.Save(key, _articleList);
                }
            }
        }
    }
}