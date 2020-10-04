using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Floo.App.Shared;
using Floo.App.Shared.Cms.Answers;
using Floo.App.Shared.Cms.Comments;
using Floo.Core.Entities.Cms.Comments;
using Floo.Core.Shared.Utils;

namespace Floo.Core.Entities.Cms.Answers
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswerService(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }
        public async Task<long> CreateAsync(AnswerDto comment, CancellationToken cancellation = default)
        {
            var entity = Mapper.Map<AnswerDto, Answer>(comment);
            var result = await _answerRepository.CreateAsync(entity, cancellation);
            return result.Id;
        }

        public async Task<ListResult<AnswerDto>> QueryListAsync(AnswerQuery query)
        {
            var result = await _answerRepository.QueryListAsync(query);
            return Mapper.Map<ListResult<Answer>, ListResult<AnswerDto>>(result);
        }
    }
}
