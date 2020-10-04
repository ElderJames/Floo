using System;
using System.Threading.Tasks;
using Floo.App.Shared.Cms.Questions;

namespace Floo.Core.Entities.Cms.Questions
{
    public class QuestionService:IQuestionService
    {
        private IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<QuestionDetailDto> QueryQuestionDetail(QuestionDetailQueryParam param)
        {
            return await _questionRepository.QueryQuestionDetail(param);
        }
    }
}
