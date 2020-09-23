﻿using Floo.Core.Entities.Cms.Contents;
using Floo.Core.Shared;

namespace Floo.Core.Entities.Cms.Questions
{
    public class Question: BaseEntity
    {
        public string Title { get; set; }

        public string Slug { get; set; }

        public long ContentId { get; set; }

        public Content Content { get; set; }
    }
}
