using System.Threading;
using System.Threading.Tasks;

namespace Floo.App.Shared.Cms.Contents
{
    public interface IContentService
    {
        public Task<ContentDto> FindAsync(long id, CancellationToken cancellation = default);

        public Task<long> CreateAsync(ContentDto Content, CancellationToken cancellation = default);

        public Task<bool> UpdateAsync(ContentDto Content, CancellationToken cancellation = default);

        public Task<ListResult<ContentDto>> QueryListAsync(ContentQuery query);
    }
}