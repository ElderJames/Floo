using System.Threading;
using System.Threading.Tasks;

namespace Floo.App.Shared.Cms.Tags
{
    public interface ITagService
    {
        public Task<TagDto> FindByIdAsync(long id, CancellationToken cancellation = default);

        public Task<long> CreateAsync(TagDto tag, CancellationToken cancellation = default);

        public Task<bool> UpdateAsync(TagDto tag, CancellationToken cancellation = default);

        public Task<ListResult<TagDto>> QueryListAsync(BaseQuery query);
    }
}