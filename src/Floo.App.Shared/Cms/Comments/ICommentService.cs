using System.Threading;
using System.Threading.Tasks;

namespace Floo.App.Shared.Cms.Comments
{
    public interface ICommentService
    {
        public Task<CommentDto> FindByIdAsync(long id, CancellationToken cancellation = default);

        public Task<long> CreateAsync(CommentDto comment, CancellationToken cancellation = default);

        public Task<bool> UpdateAsync(CommentDto comment, CancellationToken cancellation = default);

        public Task<ListResult<CommentDto>> QueryListAsync(CommentQuery query);
    }
}