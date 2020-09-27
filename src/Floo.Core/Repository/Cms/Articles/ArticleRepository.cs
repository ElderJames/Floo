using Dapper;
using Floo.Core.Entities.Cms.Articles;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Floo.Core.Repository.Cms.Articles
{
    public class ArticleRepository : IArticleRepository
    {
        private IDbConnection dbConnection;
        public ArticleRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }
        public async Task<Article> QueryById(long id)
        {
            return await dbConnection.QueryFirstAsync<Article>("select * from articles where Id=@id", new { id });
        }
    }
}
