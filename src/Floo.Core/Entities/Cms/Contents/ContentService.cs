using Floo.App.Shared;
using Floo.App.Shared.Cms.Answers;
using Floo.App.Shared.Cms.Articles;
using Floo.App.Shared.Cms.Contents;
using Floo.App.Shared.Cms.Questions;
using Floo.Core.Entities.Cms.Answers;
using Floo.Core.Entities.Cms.Articles;
using Floo.Core.Entities.Cms.Questions;
using Floo.Core.Shared.Utils;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.Core.Entities.Cms.Contents
{
    public class ContentService : IContentService
    {
        private IContentRepository _contentStorage;

        public ContentService(IContentRepository contentStorage)
        {
            _contentStorage = contentStorage;
        }

        public async Task<long> CreateAsync(ContentDto Content, CancellationToken cancellation = default)
        {
            var entity = Mapper.Map<ContentDto, Content>(Content);
            var result = await _contentStorage.CreateAsync(entity, cancellation);
            return result.Id;
        }

        public async Task<ContentDto> FindByIdAsync(long id, CancellationToken cancellation = default)
        {
            var entity = await _contentStorage.FindByIdAsync(id, cancellation);
            return Mapper.Map<Content, ContentDto>(entity);
        }

        public async Task<ListResult<ContentDto>> QueryListAsync(ContentQuery query)
        {
            var result = await _contentStorage.QueryListAsync(query);

            return new ListResult<ContentDto>(result)
            {
                Items = Mapper.Map<Content, ContentDto>(result.Items, (from, to) =>
                {
                    switch( from.Type)
                    {
                        case ContentType.Article:
                            to.Article = Mapper.Map<Article, ArticleDto>(from.Article);
                            break;

                        case ContentType.Question:
                            to.Question = Mapper.Map<Question, QuestionDto>(from.Question);
                            break;

                        case ContentType.Answer:
                            to.Answer = Mapper.Map<Answer, AnswerDto>(from.Answer);
                            break;
                    };
                   
                })
            };
        }

        public async Task<bool> UpdateAsync(ContentDto content, CancellationToken cancellation = default)
        {
            var entity = await _contentStorage.FindByIdAsync(content.Id.Value, cancellation);
            if (entity == null)
            {
                return false;
            }
            Mapper.Map(content, entity);
            return await _contentStorage.UpdateAsync(entity) > 0;
        }
    }
}