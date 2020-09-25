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
        private CommentDto _CommentModel = new CommentDto();
        protected override Task OnInitializedAsync()
        {
            MockDto();
            return Task.CompletedTask;
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
"

            };
            for (int i = 0; i < 10; i++)
            {
                _Comments.Add(new CommentDto
                {
                    Content = i.ToString()
                });
            }
        }
    }
}
