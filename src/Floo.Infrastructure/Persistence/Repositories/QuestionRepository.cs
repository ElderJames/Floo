using Floo.Core.Entities.Cms.Questions;
using Floo.Core.Shared;
using System.Linq;
using System.Threading.Tasks;
using Floo.App.Shared.Cms.Questions;

namespace Floo.Infrastructure.Persistence.Repositories
{
    public class QuestionRepository : EfRepository<Question>, IQuestionRepository
    {
        private IDbAdapter _dbAdapter;
        public QuestionRepository(IDbContext context, IIdentityContext identityContext, IDbAdapter dbAdapter) : base(context, identityContext)
        {
            _dbAdapter = dbAdapter;
        }

        public override void HandleConditions<TQuery>(ref IQueryable<Question> linq, TQuery query)
        {
        }

        public async Task<QuestionDetailDto> QueryQuestionDetail(QuestionDetailQueryParam param)
        {
            return await _dbAdapter.QueryFirstOrDefault<QuestionDetailDto>(@"SELECT qs.Id
               , qs.Title
               , qs.Slug
               , us.UserName AS Author
               , ct.TEXT AS Content
               , qs.CreatedAtUtc
               , qs.UpdatedAtUtc
               , ct.Id AS ContentId
               FROM Questions AS qs
               JOIN Contents AS ct ON qs.ContentId = ct.Id
           JOIN AspNetUsers AS us ON qs.CreatedBy = us.Id
           WHERE qs.Slug = @Slug

           AND us.UserName = @UserName", param);
        }
    }
}
