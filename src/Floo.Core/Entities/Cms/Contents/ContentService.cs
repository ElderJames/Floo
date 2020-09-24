using Floo.App.Shared;
using Floo.App.Shared.Cms.Contents;
using Floo.Core.Shared.Utils;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.Core.Entities.Cms.Contents
{
    public class ContentService : IContentService
    {
        private IEntityStorage<Content> _contentStorage;

        public ContentService(IEntityStorage<Content> contentStorage)
        {
            _contentStorage = contentStorage;
        }

        public async Task<long> CreateAsync(ContentDto Content, CancellationToken cancellation = default)
        {
            var entity = Mapper.Map<ContentDto, Content>(Content);
            var result = await _contentStorage.CreateAndSaveAsync(entity, cancellation);
            return result.Id;
        }

        public async Task<ContentDto> FindAsync(long id, CancellationToken cancellation = default)
        {
            var entity = await _contentStorage.FindAsync(cancellation, id);
            return Mapper.Map<Content, ContentDto>(entity);
        }

        public async Task<ListResult<ContentDto>> QueryListAsync(ContentQuery query)
        {
            if (query.OrderBy == null && query.OrderByDesc == null)
            {
                query.OrderByDesc = new[] { nameof(IEntity.CreatedAtUtc) };
            }

            var result = await _contentStorage.QueryAsync(query);

            return Mapper.Map<ListResult<Content>, ListResult<ContentDto>>(result);
        }

        public async Task<bool> UpdateAsync(ContentDto Content, CancellationToken cancellation = default)
        {
            var entity = await _contentStorage.FindAsync(cancellation, Content.Id);
            if (entity == null)
            {
                return false;
            }
            Mapper.Map(Content, entity);
            return await _contentStorage.UpdateAndSaveAsync(entity) > 0;
        }
    }
}