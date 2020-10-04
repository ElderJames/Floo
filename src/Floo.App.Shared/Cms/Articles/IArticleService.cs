using System.Threading;
using System.Threading.Tasks;

namespace Floo.App.Shared.Cms.Articles
{
    public interface IArticleService
    {
        public Task<ArticleDto> FindByIdAsync(long id, CancellationToken cancellation = default);

        public Task<long> CreateAsync(ArticleDto article, CancellationToken cancellation = default);

        public Task<bool> UpdateAsync(ArticleDto article, CancellationToken cancellation = default);

        public Task<ListResult<ArticleDto>> QueryListAsync(ArticleQuery query);

       Task<ArticleDetailDto> QueryArticleDetail(ArticleDetailQueryParam param);
    }
}