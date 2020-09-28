using Floo.App.Shared.Cms.Articles;
using Floo.App.Shared.Cms.Contents;
using Floo.App.Shared.Cms.Questions;
using Floo.App.Shared.Identity.User;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.App.Web.Pages
{
    public partial class Index: ComponentBase
    {
        [Parameter]
        public string ChannelName { get; set; }

        [Inject] public IArticleService articleService { get; set; }

        private List<ContentDto> _articleList = new List<ContentDto>();

        private int _pageIndex = 1;

        private ContentQuery ContentQuery = new ContentQuery();

        protected override async Task OnInitializedAsync()
        {
            await GetContents();

        }

        private async Task GetContents()
        {
            //var ContentResult = await ContentService.QueryListAsync(ContentQuery);
            //if (ContentResult.Items.Any())
            //{
            //    _ContentList.AddRange(ContentResult.Items);
            //}
            _articleList.Add(new ContentDto
            {
                Article = new ArticleDto
                {
                    Title = "文章1",
                    Summary = "文章1文章1文章1文章1文章1文章1文章1文章1文章1",
                    Cover = "https://picb.zhimg.com/v2-320009747fc474ccd71dbd87e5767b64_1440w.jpg?source=172ae18b",
                    Slug = "wenzhang1",
                },
                Author = new UserDto
                {
                    NickName = "Liu"
                }
            });
            _articleList.Add(new ContentDto
            {
                Question = new QuestionDto
                {
                    Title = "What is blazor?",
                    Summary = "文章1文章1文章1文章1文章1文章1文章1文章1文章1",
                    Cover = "https://picb.zhimg.com/v2-320009747fc474ccd71dbd87e5767b64_1440w.jpg?source=172ae18b",
                    Slug = "wenzhang1",
                },
                Author = new UserDto { UserName = "Liu" },
                Type = ContentType.Question
            });
        }

        private async Task CreateContent()
        {
            await articleService.CreateAsync(new ArticleDto
            {
                Title = "文章1",
                Summary = "文章1文章1文章1文章1文章1文章1文章1文章1文章1",
                Cover = "https://picb.zhimg.com/v2-320009747fc474ccd71dbd87e5767b64_1440w.jpg?source=172ae18b",
                Slug = "wenzhang1"
            });
        }
    }
}