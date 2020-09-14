using Floo.Core.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.App.Shared.Cms.Tags
{
    public interface ITagService
    {
        public Task<TagDto> FindAsync(long id, CancellationToken cancellation = default);

        public Task<long> CreateAsync(TagDto tag, CancellationToken cancellation = default);

        public Task<bool> UpdateAsync(TagDto tag, CancellationToken cancellation = default);

        public Task<ListResult<TagDto>> QueryListAsync(BaseQueryDto query);
    }
}