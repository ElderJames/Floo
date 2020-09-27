using Floo.Core.Entities.Cms.Comments;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Floo.Core.Repository.Cms.Comments
{
    public class CommentRepository : ICommentRepository
    {
        private IDbConnection dbConnection;
        public CommentRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }
        public Task<Comment> QueryById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
