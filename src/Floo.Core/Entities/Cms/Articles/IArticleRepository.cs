using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Floo.App.Shared.Cms.Articles;

namespace Floo.Core.Entities.Cms.Articles
{
    public interface IArticleRepository: IRepository<Article>
    {
        Task<ArticleDetailDto> QueryArticleDetail(ArticleDetailQueryParam param);
    }
}
