using Floo.Core.Entities.Cms.Questions;
using Floo.Core.Shared;
using System.Linq;

namespace Floo.Infrastructure.Persistence.Repositories
{
    public class QuestionRepository : EfRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(IDbContext context, IIdentityContext identityContext) : base(context, identityContext) { }

        public override void HandleConditions<TQuery>(ref IQueryable<Question> linq, TQuery query)
        {
        }
    }
}
