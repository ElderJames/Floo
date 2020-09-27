using Floo.App.Shared.Cms.Contents;
using Floo.Core.Entities.Cms.Articles;
using Floo.Core.Entities.Cms.Comments;
using Floo.Core.Entities.Cms.Questions;
using Floo.Core.Entities.Cms.Columns;
using Floo.Core.Entities.Cms.Tags;
using Floo.Core.Entities.Identity.Users;
using System.Collections.Generic;

namespace Floo.Core.Entities.Cms.Contents
{
    public class Content : BaseEntity
    {
        public string Text { get; set; }

        public ContentType Type { get; set; }

        public User Author { get; set; }

        public Article Article { get; set; }

        public Question Question { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<Column> Columns { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}