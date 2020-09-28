using Floo.Core.Entities.Cms.Answers;
using Floo.Core.Shared;
using System.Linq;

namespace Floo.Infrastructure.Persistence.Repositories
{
    public class AnswerRepository : EfRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(IDbContext context, IIdentityContext identityContext) : base(context, identityContext) { }

        public override void HandleConditions<TQuery>(ref IQueryable<Answer> linq, TQuery query)
        {

        }
    }
}
