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
        public string username { get; set; }
        [ParameterAttribute]
        public string slug { get; set; }
        private ArticleDto _Article;
        private List<CommentDto> _Comments = new List<CommentDto>();
        private CommentDto _CommentModel;
        private bool inEdit = false;
        private string labelOfEdit = "编辑";
        protected override Task OnInitializedAsync()
        {
            MockDto();
            InitCommentModel();
            return Task.CompletedTask;
        }

        private async Task OnEditButtonClick()
        {
            if (inEdit)
            {
                inEdit = false;
                labelOfEdit = "编辑";
            }
            else
            {
                inEdit = true;
                labelOfEdit = "保存";
            }
        }

        private async Task OnAddCommentClick()
        {
            _CommentModel.CreatedAtUtc = DateTime.Now;
            _Comments.Add(_CommentModel);
            InitCommentModel();
        }

        private void InitCommentModel()
        {
            _CommentModel = new CommentDto
            {
                Id = 999,
                Avatar = "https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png",
                Author = "Liu"
            };
        }

        private void MockDto()
        {
            _Article = new ArticleDto
            {
                Title = "第一篇文章",
                Contnet = @"# Floo 🔥

> Floo network in the Muggle world! 🧙‍♂️ 🧙‍♀️

A community site.

![Github Actions](https://img.shields.io/github/workflow/status/ElderJames/Floo/Build?style=flat-square)
[![Floo](https://img.shields.io/github/license/ElderJames/Floo?style=flat-square)](https://github.com/ElderJames/floo/blob/master/LICENSE)
[![Contributor Covenant](https://img.shields.io/badge/Contributor%20Covenant-v2.0%20adopted-ff69b4.svg?style=flat-square)](code_of_conduct.md)
[![Discord Server](https://img.shields.io/discord/758258857667067905?color=%237289DA&label=Floo&logo=discord&logoColor=white&style=flat-square)](https://discord.gg/5BCCnDZ)
",
                Author = "Liu",
                UpdatedAtUtc = DateTime.Now

            };
            for (int i = 0; i < 5; i++)
            {
                _Comments.Add(new CommentDto
                {
                    Id = i,
                    Content = "We supply a series of design principles, practical patterns and high quality design resources (Sketch and Axure), to help people create their product prototypes beautifully and efficiently.",
                    CreatedAtUtc = DateTime.Now,
                    Avatar = "https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png",
                    Author = "Liu"
                });
            }
        }
    }
}
