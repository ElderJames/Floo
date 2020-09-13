using Floo.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floo.App.Shared.Cms.Articles
{
    public class ArticleDto : BaseDto<long>
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