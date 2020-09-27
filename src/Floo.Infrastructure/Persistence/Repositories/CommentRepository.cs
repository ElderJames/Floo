using Floo.Core.Entities.Cms.Comments;
using Floo.Core.Shared;
using System.Linq;

namespace Floo.Infrastructure.Persistence.Repositories
{
    public class CommentRepository : EfRepository<Comment>, ICommentRepository
    {
        public CommentRepository(IDbContext context, IIdentityContext identityContext) : base(context, identityContext) { }


        public override void HandleConditions<TQuery>(ref IQueryable<Comment> linq, TQuery query)
        {
        }
    }
}
