using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floo.App.Shared.Cms.Questions
{
    public class QuestionQuery : BaseQuery
    {
        public QuestionQuery()
        {
            OrderByDesc = new[] { nameof(BaseDtoWithDatetime<long>.CreatedAtUtc) };
        }
    }
}
