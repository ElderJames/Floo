using Floo.App.Shared.Cms.Articles;
using Floo.App.Shared.Cms.Comments;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Floo.App.Web.Pages
{
    public partial class Question
    {
        [ParameterAttribute]
        public string username { get; set; }
        [ParameterAttribute]
        public string slug { get; set; }
        private ArticleDto _Question;
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

        private async Task OnCommitCommentClick()
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
            _Question = new ArticleDto
            {
                Title = "What is blazor?",
                Contnet = @"Blazor is a [Single Page Application](https://en.wikipedia.org/wiki/Single-page_application) development framework. The name Blazor is a combination/mutation of the words Browser and Razor (the .NET HTML view generating engine). The implication being that instead of having to execute Razor views on the server in order to present HTML to the browser, Blazor is capable of executing these views on the client.

[![Blazor client side](https://blazor-university.com/wp-content/uploads/2019/05/BlazorClientSide-300x251.png)](https://blazor-university.com/wp-content/uploads/2019/05/BlazorClientSide.png)

Blazor app with client-side execution

Blazor also supports executing [SPAs on the server](https://blazor-university.com/overview/blazor-hosting-models/).",
                Author = "Liu",
                UpdatedAtUtc = DateTime.Now

            };
            for (int i = 0; i < 2; i++)
            {
                _Comments.Add(new CommentDto
                {
                    Id = i,
                    Content = @"Interactive web UI with C#
--------------------------

Blazor lets you build interactive web UIs using C# instead of JavaScript. Blazor apps are composed of reusable web UI components implemented using C#, HTML, and CSS. Both client and server code is written in C#, allowing you to share code and libraries.

Blazor is a feature of [ASP.NET](https://dotnet.microsoft.com/apps/aspnet), the popular web development framework that extends the [.NET developer platform](https://dotnet.microsoft.com/learn/dotnet/what-is-dotnet) with tools and libraries for building web apps.

![](https://dotnet.microsoft.com/static/images/illustrations/swimlane-browser-server.svg?v=tDBC89XE_R1c14G48L_EMIh_uqTZOp7tENK0LbANSt0)

Run on WebAssembly or the server
--------------------------------

Blazor can run your client-side C# code directly in the browser, using WebAssembly. Because it's real .NET running on WebAssembly, you can re-use code and libraries from server-side parts of your application.

Alternatively, Blazor can run your client logic on the server. Client UI events are sent back to the server using SignalR - a real-time messaging framework. Once execution completes, the required UI changes are sent to the client and merged into the DOM.

![](https://dotnet.microsoft.com/static/images/illustrations/swimlane-cross-browser.svg?v=z9REeJ2Gq3kI8IT14P6ZUlYsv7Z-0jdF1LUgZubkh1w)",
                    CreatedAtUtc = DateTime.Now,
                    Avatar = "https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png",
                    Author = "Liu"
                });
            }
        }
    }
}
