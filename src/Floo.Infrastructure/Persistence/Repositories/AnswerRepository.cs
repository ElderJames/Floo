using Floo.Core.Entities.Cms.Answers;
using Floo.Core.Shared;
using System.Linq;
using Floo.App.Shared.Cms.Answers;

namespace Floo.Infrastructure.Persistence.Repositories
{
    public class AnswerRepository : EfRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(IDbContext context, IIdentityContext identityContext) : base(context, identityContext) { }

        public override void HandleConditions<TQuery>(ref IQueryable<Answer> linq, TQuery query)
        {
            if (query is AnswerQuery answerQuery)
            {
                linq = linq.Where(l => l.ContentId == answerQuery.ContentId);
            }
        }
    }
}
