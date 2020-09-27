using Floo.Core.Entities.Cms.Channels;
using System.Data;
using System.Threading.Tasks;

namespace Floo.Core.Repository.Cms.Channels
{
    public class ChannelRepository : IChannelRepository
    {
        private IDbConnection dbConnection;
        public ChannelRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }
        public Task<Channel> QueryById(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
