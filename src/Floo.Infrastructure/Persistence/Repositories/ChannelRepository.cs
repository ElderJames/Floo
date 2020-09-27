using Floo.Core.Entities.Cms.Channels;
using Floo.Core.Shared;
using System.Linq;

namespace Floo.Infrastructure.Persistence.Repositories
{
    public class ChannelRepository : EfRepository<Channel>, IChannelRepository
    {
        public ChannelRepository(IDbContext context, IIdentityContext identityContext) : base(context, identityContext) { }

        public override void HandleConditions<TQuery>(ref IQueryable<Channel> linq, TQuery query)
        {
          
        }
    }
}
