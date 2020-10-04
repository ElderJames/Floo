using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floo.App.Shared.Cms.Answers
{
    public class AnswerDto : BaseDtoWithDatetime<long?>
    {
        public long? ReplyId { get; set; }

        public long? ContentId { get; set; }

        public string Author { get; set; }
        public string Avatar { get; set; }

        public string Content { get; set; }
    }
}
