using Floo.App.Shared.Cms.Channels;
using Floo.Core.Shared;
using Floo.Core.Shared.Utils;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.Core.Entities.Cms.Channels
{
    public class ChannelService : IChannelService
    {
        private IEntityStorage<Channel> _channelStorage;

        public ChannelService(IEntityStorage<Channel> channelStorage)
        {
            _channelStorage = channelStorage;
        }

        public async Task<long> CreateAsync(ChannelDto channel, CancellationToken cancellation = default)
        {
            var entity = Mapper.Map<ChannelDto, Channel>(channel);
            var result = await _channelStorage.CreateAndSaveAsync(entity, cancellation);
            return result.Id;
        }

        public async Task<ChannelDto> FindAsync(long id, CancellationToken cancellation = default)
        {
            var entity = await _channelStorage.FindAsync(cancellation, id);
            return Mapper.Map<Channel, ChannelDto>(entity);
        }

        public async Task<ListResult<ChannelDto>> QueryListAsync(BaseQueryDto query)
        {
            var result = await _channelStorage.QueryAsync(query);
            return Mapper.Map<ListResult<Channel>, ListResult<ChannelDto>>(result);
        }

        public async Task<bool> UpdateAsync(ChannelDto channel, CancellationToken cancellation = default)
        {
            var entity = await _channelStorage.FindAsync(cancellation, channel.Id);
            if (entity == null)
            {
                return false;
            }
            Mapper.Map(channel, entity);
            return await _channelStorage.UpdateAndSaveAsync(entity) > 0;
        }
    }
}