using Floo.App.Shared;
using Floo.App.Shared.Cms.Tags;
using Floo.Core.Shared.Utils;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.Core.Entities.Cms.Tags
{
    public class TagService : ITagService
    {
        private ITagRepository _tagStorage;

        public TagService(ITagRepository tagStorage)
        {
            _tagStorage = tagStorage;
        }

        public async Task<long> CreateAsync(TagDto tag, CancellationToken cancellation = default)
        {
            var entity = Mapper.Map<TagDto, Tag>(tag);
            var result = await _tagStorage.CreateAsync(entity, cancellation);
            return result.Id;
        }

        public async Task<TagDto> FindByIdAsync(long id, CancellationToken cancellation = default)
        {
            var entity = await _tagStorage.FindByIdAsync(id, cancellation);
            return Mapper.Map<Tag, TagDto>(entity);
        }

        public async Task<ListResult<TagDto>> QueryListAsync(BaseQuery query)
        {
            var result = await _tagStorage.QueryListAsync(query);
            return Mapper.Map<ListResult<Tag>, ListResult<TagDto>>(result);
        }

        public async Task<bool> UpdateAsync(TagDto tag, CancellationToken cancellation = default)
        {
            var entity = await _tagStorage.FindByIdAsync(tag.Id, cancellation);
            if (entity == null)
            {
                return false;
            }
            Mapper.Map(tag, entity);
            return await _tagStorage.UpdateAsync(entity) > 0;
        }
    }
}