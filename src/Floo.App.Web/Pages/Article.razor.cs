using Floo.App.Shared.Cms.Articles;
using Floo.App.Shared.Cms.Comments;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Floo.App.Web.Pages
{
    public partial class Article
    {
        [ParameterAttribute]
        public string Username { get; set; }
        [ParameterAttribute]
        public string Slug { get; set; }
        [Inject]
        public IArticleService ArticleService { get; set; }
        [Inject]
        public ICommentService CommentService { get; set; }
        private ArticleDetailDto _article;
        private List<CommentDto> _comments = new List<CommentDto>();
        private CommentDto _commentModel;
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

        private async Task OnAddCommentClick()
        {
            _commentModel.CreatedAtUtc = DateTime.Now;
            var id = await CommentService.CreateAsync(_commentModel);
            _commentModel.Id = id;
            _comments.Add(_commentModel);
            InitCommentModel();
        }

        private void InitCommentModel()
        {
            _commentModel = new CommentDto();
        }

        private async Task SetData()
        {
            _article = await ArticleService.QueryArticleDetail(new ArticleDetailQueryParam
            {
                Slug = Slug,
                UserName = Username
            });
            _comments = (await CommentService.QueryListAsync(new CommentQuery
            {
                ContentId = _article.ContentId
            })).Items.ToList();

        }
    }
}
