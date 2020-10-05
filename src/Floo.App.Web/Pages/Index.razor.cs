using Floo.App.Shared.Cms.Articles;
using Floo.App.Shared.Cms.Contents;
using Floo.App.Shared.Cms.Questions;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Floo.App.Shared.Identity.User;

namespace Floo.App.Web.Pages
{
    public partial class Index : ComponentBase
    {
        [Parameter]
        public string ChannelName { get; set; }

        [Inject] public IArticleService ArticleService { get; set; }
        [Inject] public IQuestionService QuestionService { get; set; }

        private List<ContentDto> _contentList = new List<ContentDto>();

        private int _pageIndex = 1;

        private readonly ArticleQuery _articleQuery = new ArticleQuery();
        private readonly QuestionQuery _questionQuery = new QuestionQuery();

        protected override async Task OnInitializedAsync()
        {
            await GetContents();
        }

        private async Task GetContents()
        {
            var articleResult = await ArticleService.QueryListAsync(_articleQuery);
            if (articleResult.Items.Any())
            {
                _contentList.AddRange(articleResult.Items.Select(u => new ContentDto
                {
                    Text = u.Contnet.Text,
                    Type = ContentType.Article,
                    Article = u,
                    Author = MockUser()
                }));
            }

            var questionResult = await QuestionService.QueryListAsync(_questionQuery);
            if (questionResult.Items.Any())
            {
                _contentList.AddRange(questionResult.Items.Select(u => new ContentDto
                {
                    Text = u.Summary,
                    Type = ContentType.Question,
                    Question = u,
                    Author = MockUser()
                }));
            }
        }

        private UserDto MockUser()
        {
            return new UserDto
            {
                UserName = "Liu"
            };
        }
    }
}