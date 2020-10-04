using Floo.App.Shared.Cms.Contents;
using Floo.App.Shared.Identity.User;
using Floo.Core.Shared;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Floo.App.Shared.Cms.Articles;

namespace Floo.App.Web.Pages
{
    public partial class Writing : ComponentBase
    {
        [Parameter]
        public string ContentSlug { get; set; }

        [Parameter]
        public string UserName { get; set; }

        [Inject] 
        public IContentService ContentService { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public IIdentityContext IdentityContext { get; set; }

        private readonly ContentDto _content = new ContentDto
        {
            Article = new ArticleDto()
        };

        private async Task CreateOrUpdateContent()
        {
            if (_content.Id.HasValue)
            {
                await ContentService.UpdateAsync(_content);
            }
            else
            {
                await ContentService.CreateAsync(_content);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(ContentSlug))
            {
                if (UserName== IdentityContext.UserName)
                {
                
                }
            }

            await base.OnInitializedAsync();
        }
    }
}
