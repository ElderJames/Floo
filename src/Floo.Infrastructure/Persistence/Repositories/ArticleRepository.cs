using Floo.App.Shared.Cms.Articles;
using Floo.Core.Entities.Cms.Articles;
using Floo.Core.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Floo.Infrastructure.Persistence.Repositories
{
    public class ArticleRepository : EfRepository<Article>, IArticleRepository
    {
        private readonly IDbAdapter _dbAdapter;
        public ArticleRepository(IDbContext context, IIdentityContext identityContext, IDbAdapter dbAdapter) : base(context, identityContext)
        {
            _dbAdapter = dbAdapter;
        }

        public override IQueryable<Article> DbSet => Context.Set<Article>().Include(x => x.Content).Include(x=>x.Channel).Include(x=>x.Column);

        public override void HandleConditions<TQuery>(ref IQueryable<Article> linq, TQuery query)
        {
            var articleQuery = query as ArticleQuery;

            linq.WhereIf(x => x.ChannelId == articleQuery.ChannelId, articleQuery.ChannelId.HasValue);
        }

        public async Task<ArticleDetailDto> QueryArticleDetail(ArticleDetailQueryParam param)
        {
            return await _dbAdapter.QueryFirstOrDefault<ArticleDetailDto>(@"SELECT ar.Id
	,ar.Title
	,ar.Slug
	,ar.Summary
	,ar.Cover
	,us.UserName AS Author
	,ch.Name AS ChannelName
	,ct.TEXT AS Content
	,ar.CreatedAtUtc
	,ar.UpdatedAtUtc
	,ct.Id AS ContentId
FROM Articles AS ar
JOIN Contents AS ct ON ar.ContentId = ct.Id
JOIN AspNetUsers AS us ON ar.CreatedBy = us.Id
JOIN Channels AS ch ON ar.ChannelId = ch.Id
WHERE ar.Slug = @Slug
	AND us.UserName = @UserName", param);
        }
    }
}
