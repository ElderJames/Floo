using Floo.App.Shared.Cms.Articles;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        private async Task CreateArticle()
        {
            await ArticleService.CreateAsync(new ArticleDto
            {
                Title = "文章1",
                Summary= "文章1文章1文章1文章1文章1文章1文章1文章1文章1",
                Cover= "https://picb.zhimg.com/v2-320009747fc474ccd71dbd87e5767b64_1440w.jpg?source=172ae18b",
                Slug="wenzhang1"
            });
        }
    }
}