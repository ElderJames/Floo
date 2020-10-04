using Floo.App.Shared;
using Floo.App.Shared.Cms.Comments;
using Floo.Core.Shared.Utils;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.Core.Entities.Cms.Comments
{
    public class CommentService : ICommentService
    {
        private ICommentRepository _commentStorage;

        public CommentService(ICommentRepository commentStorage)
        {
            _commentStorage = commentStorage;
        }

        public async Task<long> CreateAsync(CommentDto comment, CancellationToken cancellation = default)
        {
            var entity = Mapper.Map<CommentDto, Comment>(comment);
            var result = await _commentStorage.CreateAsync(entity, cancellation);
            return result.Id;
        }

        public async Task<CommentDto> FindByIdAsync(long id, CancellationToken cancellation = default)
        {
            var entity = await _commentStorage.FindByIdAsync( id, cancellation);
            return Mapper.Map<Comment, CommentDto>(entity);
        }

        public async Task<ListResult<CommentDto>> QueryListAsync(CommentQuery query)
        {
            var result = await _commentStorage.QueryListAsync(query);
            return Mapper.Map<ListResult<Comment>, ListResult<CommentDto>>(result);
        }

        public async Task<bool> UpdateAsync(CommentDto comment, CancellationToken cancellation = default)
        {
            var entity = await _commentStorage.FindByIdAsync( comment.Id,cancellation);
            if (entity == null)
            {
                return false;
            }
            Mapper.Map(comment, entity);
            return await _commentStorage.UpdateAsync(entity) > 0;
        }
    }
}