using System.Threading;
using System.Threading.Tasks;

namespace Floo.App.Shared.Cms.Questions
{
  public  interface IQuestionService
    {
        public Task<ListResult<QuestionDto>> QueryListAsync(QuestionQuery query);
        Task<long> CreateAsync(QuestionDto article, CancellationToken cancellation = default);
        Task<QuestionDetailDto> QueryQuestionDetail(QuestionDetailQueryParam param);
    }
}
