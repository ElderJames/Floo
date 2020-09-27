using Floo.Core.Entities.Cms.Questions;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Floo.Core.Repository.Cms.Questions
{
    public class QuestionRepository : IQuestionRepository
    {
        private IDbConnection dbConnection;
        public QuestionRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }
        public Task<Question> QueryById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
