using System.Threading;
using System.Threading.Tasks;

namespace Floo.App.Shared.Cms.Answers
{
    public interface IAnswerService
    {
        Task<long> CreateAsync(AnswerDto comment, CancellationToken cancellation = default);
        public Task<ListResult<AnswerDto>> QueryListAsync(AnswerQuery query);
    }
}