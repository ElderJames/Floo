using Floo.Core.Entities.Cms.Comments;
using Floo.Core.Shared;
using System.Linq;
using Floo.App.Shared.Cms.Comments;

namespace Floo.Infrastructure.Persistence.Repositories
{
    public class CommentRepository : EfRepository<Comment>, ICommentRepository
    {
        public CommentRepository(IDbContext context, IIdentityContext identityContext) : base(context, identityContext) { }


        public override void HandleConditions<TQuery>(ref IQueryable<Comment> linq, TQuery query)
        {
            if (query is CommentQuery commentQuery)
            {
                linq = linq.Where(l => l.ContentId == commentQuery.ContentId);
            }
        }
    }
}
