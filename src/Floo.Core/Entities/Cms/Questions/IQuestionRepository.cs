using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Floo.App.Shared.Cms.Questions;

namespace Floo.Core.Entities.Cms.Questions
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<QuestionDetailDto> QueryQuestionDetail(QuestionDetailQueryParam param);
    }
}
