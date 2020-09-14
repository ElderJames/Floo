using Floo.Core.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.App.Shared.Cms.Articles
{
    public interface IArticleService
    {
        public Task<ArticleDto> FindAsync(long id, CancellationToken cancellation = default);

        public Task<long> CreateAsync(ArticleDto article, CancellationToken cancellation = default);

        public Task<bool> UpdateAsync(ArticleDto article, CancellationToken cancellation = default);

        public Task<ListResult<ArticleDto>> QueryListAsync(BaseQueryDto query);
    }
}