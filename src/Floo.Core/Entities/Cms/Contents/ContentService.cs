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
        private IContentRepository _contentStorage;

        public ContentService(IContentRepository contentStorage)
        {
            _contentStorage = contentStorage;
        }

        public async Task<long> CreateAsync(ContentDto Content, CancellationToken cancellation = default)
        {
            var entity = Mapper.Map<ContentDto, Content>(Content);
            var result = await _contentStorage.CreateAsync(entity, cancellation);
            return result.Id;
        }

        public async Task<ContentDto> FindByIdAsync(long id, CancellationToken cancellation = default)
        {
            var entity = await _contentStorage.FindByIdAsync(id, cancellation);
            return Mapper.Map<Content, ContentDto>(entity);
        }

        public async Task<ListResult<ContentDto>> QueryListAsync(ContentQuery query)
        {
            var result = await _contentStorage.QueryListAsync(query);

            return Mapper.Map<ListResult<Content>, ListResult<ContentDto>>(result);
        }

        public async Task<bool> UpdateAsync(ContentDto content, CancellationToken cancellation = default)
        {
            var entity = await _contentStorage.FindByIdAsync(content.Id.Value, cancellation);
            if (entity == null)
            {
                return false;
            }
            Mapper.Map(content, entity);
            return await _contentStorage.UpdateAsync(entity) > 0;
        }
    }
}