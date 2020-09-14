using Floo.App.Shared.Cms.Comments;
using Floo.Core.Shared;
using Floo.Core.Shared.Utils;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.Core.Entities.Cms.Comments
{
    public class CommentService : ICommentService
    {
        private IEntityStorage<Comment> _commentStorage;

        public CommentService(IEntityStorage<Comment> commentStorage)
        {
            _commentStorage = commentStorage;
        }

        public async Task<long> CreateAsync(CommentDto comment, CancellationToken cancellation = default)
        {
            var entity = Mapper.Map<CommentDto, Comment>(comment);
            var result = await _commentStorage.CreateAndSaveAsync(entity, cancellation);
            return result.Id;
        }

        public async Task<CommentDto> FindAsync(long id, CancellationToken cancellation = default)
        {
            var entity = await _commentStorage.FindAsync(cancellation, id);
            return Mapper.Map<Comment, CommentDto>(entity);
        }

        public async Task<ListResult<CommentDto>> QueryListAsync(BaseQuery query)
        {
            var result = await _commentStorage.QueryAsync(query);
            return Mapper.Map<ListResult<Comment>, ListResult<CommentDto>>(result);
        }

        public async Task<bool> UpdateAsync(CommentDto comment, CancellationToken cancellation = default)
        {
            var entity = await _commentStorage.FindAsync(cancellation, comment.Id);
            if (entity == null)
            {
                return false;
            }
            Mapper.Map(comment, entity);
            return await _commentStorage.UpdateAndSaveAsync(entity) > 0;
        }
    }
}