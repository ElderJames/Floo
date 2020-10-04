using Floo.App.Shared;
using Floo.App.Shared.Cms.Articles;
using Floo.App.Shared.Cms.Contents;
using Floo.Core.Entities.Cms.Contents;
using Floo.Core.Shared.Utils;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.Core.Entities.Cms.Articles
{
    public class ArticleService : IArticleService
    {
        private IArticleRepository _articleStorage;

        public ArticleService(IArticleRepository articleStorage)
        {
            _articleStorage = articleStorage;
        }

        public async Task<long> CreateAsync(ArticleDto article, CancellationToken cancellation = default)
        {
            var entity = Mapper.Map<ArticleDto, Article>(article);
            var result = await _articleStorage.CreateAsync(entity, cancellation);
            return result.Id;
        }

        public async Task<ArticleDto> FindByIdAsync(long id, CancellationToken cancellation = default)
        {
            var entity = await _articleStorage.FindByIdAsync(id, cancellation);
            return Mapper.Map<Article, ArticleDto>(entity);
        }

        public async Task<ListResult<ArticleDto>> QueryListAsync(ArticleQuery query)
        {
            var result = await _articleStorage.QueryListAsync(query);
            return new ListResult<ArticleDto>(result)
            {
                Items = Mapper.Map<Article, ArticleDto>(result.Items, (from, to) =>
                {
                    to.Contnet = Mapper.Map<Content, ContentDto>(from.Content);
                })
            };
        }

        public async Task<ArticleDetailDto> QueryArticleDetail(ArticleDetailQueryParam param)
        {
            return await _articleStorage.QueryArticleDetail(param);
        }

        public async Task<bool> UpdateAsync(ArticleDto article, CancellationToken cancellation = default)
        {
            var entity = await _articleStorage.FindByIdAsync(article.Id, cancellation);
            if (entity == null)
            {
                return false;
            }

            Mapper.Map(article, entity);
            return await _articleStorage.UpdateAsync(entity) > 0;
        }
    }
}