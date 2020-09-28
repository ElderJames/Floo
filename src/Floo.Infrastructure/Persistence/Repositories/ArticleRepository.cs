using Floo.App.Shared.Cms.Articles;
using Floo.Core.Entities.Cms.Articles;
using Floo.Core.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Floo.Infrastructure.Persistence.Repositories
{
    public class ArticleRepository : EfRepository<Article>, IArticleRepository
    {
        public ArticleRepository(IDbContext context, IIdentityContext identityContext) : base(context, identityContext) { }

        public override IQueryable<Article> DbSet => Context.Set<Article>().Include(x => x.Contnet).Include(x=>x.Channel).Include(x=>x.Column);

        public override void HandleConditions<TQuery>(ref IQueryable<Article> linq, TQuery query)
        {
            var articleQuery = query as ArticleQuery;

            linq.WhereIf(x => x.ChannelId == articleQuery.ChannelId, articleQuery.ChannelId.HasValue);
        }
    }
}
