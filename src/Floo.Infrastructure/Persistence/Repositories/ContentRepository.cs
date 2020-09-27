using Floo.Core.Entities.Cms.Contents;
using Floo.Core.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Floo.Infrastructure.Persistence.Repositories
{
    public class ContentRepository : EfRepository<Content>, IContentRepository
    {
        public ContentRepository(IDbContext context, IIdentityContext identityContext) : base(context, identityContext) { }

        public override IQueryable<Content> DbSet => base.DbSet.Include(x => x.Article);

        public override void HandleConditions<TQuery>(ref IQueryable<Content> linq, TQuery query)
        {
        }
    }
}
