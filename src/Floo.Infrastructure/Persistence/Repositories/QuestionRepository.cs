using Floo.Core.Entities.Cms.Questions;
using Floo.Core.Shared;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Floo.App.Shared.Cms.Contents;
using Floo.App.Shared.Cms.Questions;
using Floo.Core.Entities.Cms.Contents;
using Microsoft.EntityFrameworkCore;

namespace Floo.Infrastructure.Persistence.Repositories
{
    public class QuestionRepository : EfRepository<Question>, IQuestionRepository
    {
        private IDbAdapter _dbAdapter;
        private IContentRepository _contentRepository;
        public QuestionRepository(IDbContext context, IIdentityContext identityContext, IDbAdapter dbAdapter, IContentRepository contentRepository) : base(context, identityContext)
        {
            _dbAdapter = dbAdapter;
            _contentRepository = contentRepository;
        }

        public override void HandleConditions<TQuery>(ref IQueryable<Question> linq, TQuery query)
        {
            linq = linq.Include(u => u.Content);
        }

        public override async Task<Question> CreateAsync(Question entity, CancellationToken cancellationToken = default)
        {
            entity.Content.Type = ContentType.Question;
            var content = await _contentRepository.CreateAsync(entity.Content, cancellationToken);
            entity.ContentId = content.Id;
            return await base.CreateAsync(entity, cancellationToken);
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
