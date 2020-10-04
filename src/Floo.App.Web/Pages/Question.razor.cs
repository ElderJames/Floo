using Floo.App.Shared.Cms.Articles;
using Floo.App.Shared.Cms.Comments;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Floo.App.Shared.Cms.Answers;
using Floo.App.Shared.Cms.Questions;

namespace Floo.App.Web.Pages
{
    public partial class Question
    {
        [ParameterAttribute]
        public string Username { get; set; }
        [ParameterAttribute]
        public string Slug { get; set; }
        [Inject]
        public IQuestionService QuestionService { get; set; }
        [Inject]
        public IAnswerService AnswerService { get; set; }
        private QuestionDetailDto _question=new QuestionDetailDto();
        private List<AnswerDto> _answers = new List<AnswerDto>();
        private AnswerDto _answerModel=new AnswerDto();
        private bool _inEdit;
        private string _labelOfEdit = "编辑";
        protected override async Task OnInitializedAsync()
        {
            await SetData();
            InitCommentModel();
        }

        private void OnEditButtonClick()
        {
            if (_inEdit)
            {
                _inEdit = false;
                _labelOfEdit = "编辑";
            }
            else
            {
                _inEdit = true;
                _labelOfEdit = "保存";
            }
        }

        private async Task OnCommitCommentClick()
        {
            _answerModel.CreatedAtUtc = DateTime.Now;
            var id = await AnswerService.CreateAsync(_answerModel);
            _answerModel.Id = id;
            _answers.Add(_answerModel);
            InitCommentModel();
        }

        private void InitCommentModel()
        {
            _answerModel = new AnswerDto();
        }

        private async Task SetData()
        {
            _question = await QuestionService.QueryQuestionDetail(new QuestionDetailQueryParam
            {
                Slug = Slug,
                UserName = Username
            });
            if (_question != null)
            {
                _answers = (await AnswerService.QueryListAsync(new AnswerQuery
                {
                    ContentId = _question.ContentId
                })).Items.ToList();
            }
        }
    }
}
