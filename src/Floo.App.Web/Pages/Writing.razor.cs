using Floo.App.Shared.Cms.Articles;
using Floo.App.Shared.Identity.User;
using Floo.Core.Shared;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Floo.App.Web.Pages
{
    public partial class Writing : ComponentBase
    {
        [Parameter]
        public string ArticleSlug { get; set; }

        [Parameter]
        public string UserName { get; set; }

        [Inject] 
        public IArticleService ArticleService { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public IIdentityContext IdentityContext { get; set; }

        private ArticleDto _article = new ArticleDto();

        private async Task CreateOrUpdateArticle()
        {
            if (_article.Id.HasValue)
            {
                await ArticleService.UpdateAsync(_article);
            }
            else
            {
                await ArticleService.CreateAsync(_article);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(ArticleSlug))
            {
                if (UserName== IdentityContext.UserName)
                {
                
                }
            }

            await base.OnInitializedAsync();
        }
    }
}
