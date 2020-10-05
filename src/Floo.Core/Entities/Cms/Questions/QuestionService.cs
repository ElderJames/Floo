using System;
using System.Threading;
using System.Threading.Tasks;
using Floo.App.Shared;
using Floo.App.Shared.Cms.Articles;
using Floo.App.Shared.Cms.Questions;
using Floo.Core.Entities.Cms.Articles;
using Floo.Core.Entities.Cms.Contents;
using Floo.Core.Shared.Utils;

namespace Floo.Core.Entities.Cms.Questions
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<ListResult<QuestionDto>> QueryListAsync(QuestionQuery query)
        {
            var result = await _questionRepository.QueryListAsync(query);
            return new ListResult<QuestionDto>(result)
            {
                Items = Mapper.Map<Question, QuestionDto>(result.Items, (from, to) =>
                {
                    to.Summary = from.Content.Text;
                })
            };
        }

        public async Task<long> CreateAsync(QuestionDto question, CancellationToken cancellation = default)
        {
            var entity = Mapper.Map<QuestionDto, Question>(question);
            entity.Content = new Content
            {
                Text = question.Summary
            };
            var result = await _questionRepository.CreateAsync(entity, cancellation);
            return result.Id;
        }

        public async Task<QuestionDetailDto> QueryQuestionDetail(QuestionDetailQueryParam param)
        {
            return await _questionRepository.QueryQuestionDetail(param);
        }
    }
}
