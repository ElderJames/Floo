﻿using Floo.Core.Shared;

namespace Floo.Core.Entities.Cms.Articles
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }

        public string Slug { get; set; }

        public string Summary { get; set; }

        public string Contnet { get; set; }

        public string Cover { get; set; }

        public string Source { get; set; }

        public long? ChannelId { get; set; }

        public long? SpecialColumnId { get; set; }
    }
}