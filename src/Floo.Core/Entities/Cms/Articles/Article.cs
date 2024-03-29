﻿using Floo.Core.Entities.Cms.Channels;
using Floo.Core.Entities.Cms.Contents;
using Floo.Core.Entities.Cms.Columns;

namespace Floo.Core.Entities.Cms.Articles
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }

        public string Slug { get; set; }

        public string Summary { get; set; }
      
        public string Cover { get; set; }

        public string Source { get; set; }

        public long ChannelId { get; set; }

        public long ContentId { get; set; }

        public Channel Channel { get; set; }

        public Column Column { get; set; }

        public Content Content { get; set; }
    }
}