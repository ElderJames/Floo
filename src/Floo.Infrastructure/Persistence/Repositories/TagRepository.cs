using Floo.Core.Entities.Cms.Tags;
using Floo.Core.Shared;
using System.Linq;

namespace Floo.Infrastructure.Persistence.Repositories
{
    public class TagRepository : EfRepository<Tag>, ITagRepository
    {
        public TagRepository(IDbContext context, IIdentityContext identityContext) : base(context, identityContext) { }

        public override void HandleConditions<TQuery>(ref IQueryable<Tag> linq, TQuery query)
        {
        }
    }
}
