using Floo.Core.Entities.Cms.Answers;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Floo.Core.Repository.Cms.Answers
{
    public class AnswerRepository : IAnswerRepository
    {
        private IDbConnection dbConnection;
        public AnswerRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }
        public Task<Answer> QueryById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
