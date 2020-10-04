using System.Threading.Tasks;

namespace Floo.App.Shared.Cms.Questions
{
  public  interface IQuestionService
    {
        Task<QuestionDetailDto> QueryQuestionDetail(QuestionDetailQueryParam param);
    }
}
