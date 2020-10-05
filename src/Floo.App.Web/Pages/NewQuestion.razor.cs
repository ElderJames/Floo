using Floo.App.Shared.Cms.Contents;
using Floo.App.Shared.Identity.User;
using Floo.Core.Shared;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Floo.App.Shared.Cms.Questions;

namespace Floo.App.Web.Pages
{
    public partial class NewQuestion : ComponentBase
    {
        [Inject]
        public IQuestionService QuestionService { get; set; }

        private readonly QuestionDto _question = new QuestionDto();

        private async Task CreateOrUpdateContent()
        {
            if (_question.Id.HasValue)
            {
                //await QuestionService.UpdateAsync(_question);
            }
            else
            {
                await QuestionService.CreateAsync(_question);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
    }
}