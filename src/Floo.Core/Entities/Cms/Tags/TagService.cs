using Floo.App.Shared.Cms.Tags;
using Floo.Core.Shared;
using Floo.Core.Shared.Utils;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.Core.Entities.Cms.Tags
{
    public class TagService : ITagService
    {
        private IEntityStorage<Tag> _tagStorage;

        public TagService(IEntityStorage<Tag> tagStorage)
        {
            _tagStorage = tagStorage;
        }

        public async Task<long> CreateAsync(TagDto tag, CancellationToken cancellation = default)
        {
            var entity = Mapper.Map<TagDto, Tag>(tag);
            var result = await _tagStorage.CreateAndSaveAsync(entity, cancellation);
            return result.Id;
        }

        public async Task<TagDto> FindAsync(long id, CancellationToken cancellation = default)
        {
            var entity = await _tagStorage.FindAsync(cancellation, id);
            return Mapper.Map<Tag, TagDto>(entity);
        }

        public async Task<ListResult<TagDto>> QueryListAsync(BaseQuery query)
        {
            var result = await _tagStorage.QueryAsync(query);
            return Mapper.Map<ListResult<Tag>, ListResult<TagDto>>(result);
        }

        public async Task<bool> UpdateAsync(TagDto tag, CancellationToken cancellation = default)
        {
            var entity = await _tagStorage.FindAsync(cancellation, tag.Id);
            if (entity == null)
            {
                return false;
            }
            Mapper.Map(tag, entity);
            return await _tagStorage.UpdateAndSaveAsync(entity) > 0;
        }
    }
}