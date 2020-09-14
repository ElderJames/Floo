using Floo.App.Shared.Cms.Articles;
using Floo.Core.Shared;
using Floo.Core.Shared.Utils;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.Core.Entities.Cms.Articles
{
    public class ArticleService : IArticleService
    {
        private IEntityStorage<Article> _articleStorage;

        public ArticleService(IEntityStorage<Article> articleStorage)
        {
            _articleStorage = articleStorage;
        }

        public async Task<long> CreateAsync(ArticleDto article, CancellationToken cancellation = default)
        {
            var entity = Mapper.Map<ArticleDto, Article>(article);
            var result = await _articleStorage.CreateAndSaveAsync(entity, cancellation);
            return result.Id;
        }

        public async Task<ArticleDto> FindAsync(long id, CancellationToken cancellation = default)
        {
            var entity = await _articleStorage.FindAsync(cancellation, id);
            return Mapper.Map<Article, ArticleDto>(entity);
        }

        public async Task<ListResult<ArticleDto>> QueryListAsync(ArticleQuery query)
        {
            var result = await _articleStorage.QueryAsync(query, linq =>
            {
                linq = linq.Where(x => x.ChannelId == query.ChannelId)
                .Where(x => x.Title.Contains(query.Title));
            });
            return Mapper.Map<ListResult<Article>, ListResult<ArticleDto>>(result);
        }

        public async Task<bool> UpdateAsync(ArticleDto article, CancellationToken cancellation = default)
        {
            var entity = await _articleStorage.FindAsync(cancellation, article.Id);
            if (entity == null)
            {
                return false;
            }
            Mapper.Map(article, entity);
            return await _articleStorage.UpdateAndSaveAsync(entity) > 0;
        }
    }
}