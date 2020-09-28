using Floo.App.Shared.Cms.Articles;
using Floo.App.Shared.Cms.Channels;
using Floo.App.Shared.Cms.Comments;
using Floo.App.Shared.Cms.Contents;
using Floo.App.Shared.Cms.SpecialColumns;
using Floo.App.Shared.Cms.Tags;
using Floo.Core.Entities.Cms.Articles;
using Floo.Core.Entities.Cms.Channels;
using Floo.Core.Entities.Cms.Columns;
using Floo.Core.Entities.Cms.Comments;
using Floo.Core.Entities.Cms.Contents;
using Floo.Core.Entities.Cms.Tags;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFlooCore(this IServiceCollection services)
        {
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IContentService, ContentService>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IChannelService, ChannelService>();
            services.AddScoped<IColumnService, ColumnService>();
            services.AddScoped<ITagService, TagService>();

            return services;
        }
    }
}
