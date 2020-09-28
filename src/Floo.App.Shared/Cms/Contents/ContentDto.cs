﻿using Floo.App.Shared.Cms.Answers;
using Floo.App.Shared.Cms.Articles;
using Floo.App.Shared.Cms.Comments;
using Floo.App.Shared.Cms.Questions;
using Floo.App.Shared.Cms.SpecialColumns;
using Floo.App.Shared.Cms.Tags;
using Floo.App.Shared.Identity.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Floo.App.Shared.Cms.Contents
{
    public class ContentDto : BaseDtoWithDatetime<long?>
    {
        public string Text { get; set; }

        public ContentType Type { get; set; }

        public UserDto Author { get; set; }

        [NotMapped]
        public ArticleDto Article { get; set; }

        [NotMapped]
        public AnswerDto Answer { get; set; }

        [NotMapped]
        public QuestionDto Question { get; set; }

        public ICollection<TagDto> Tags { get; set; }

        public ICollection<ColumnDto> Columns { get; set; }

        public ICollection<CommentDto> Comments { get; set; }

        public string Url
        {
            get
            {
                switch (Type)
                {
                    case ContentType.Question:
                        return $"/question/{Question?.Slug}";
                    case ContentType.Answer:
                        return $"/question/{Question?.Slug}/{Answer?.Id}";
                    case ContentType.Article:
                        return $"/{Author?.UserName}/{Article?.Slug}";

                    default:
                        return "";
                }
            }
        }
    }
}