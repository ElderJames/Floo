using Floo.Core.Entities.Cms.Answers;
using Floo.Core.Entities.Cms.Articles;
using Floo.Core.Entities.Cms.Channels;
using Floo.Core.Entities.Cms.Columns;
using Floo.Core.Entities.Cms.Comments;
using Floo.Core.Entities.Cms.Contents;
using Floo.Core.Entities.Cms.Questions;
using Floo.Core.Entities.Cms.Tags;
using Floo.Core.Shared;
using Floo.Infrastructure.Persistence;
using Floo.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Floo.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFlooEntityStorage<TDbContext>(this IServiceCollection services)
            where TDbContext : class, IDbContext
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IIdentityContext>(sp => new IdentityContext(sp.GetService<IHttpContextAccessor>()));
            services.AddScoped<IDbContext, TDbContext>();

            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IContentRepository, ContentRepository>();
            services.AddScoped<IChannelRepository, ChannelRepository>();
            services.AddScoped<IColumnRepository, ColumnRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            return services;
        }
    }
}