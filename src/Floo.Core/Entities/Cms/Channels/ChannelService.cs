using Floo.App.Shared;
using Floo.App.Shared.Cms.Channels;
using Floo.Core.Shared.Utils;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.Core.Entities.Cms.Channels
{
    public class ChannelService : IChannelService
    {
        private IChannelRepository _channelStorage;

        public ChannelService(IChannelRepository channelStorage)
        {
            _channelStorage = channelStorage;
        }

        public async Task<long> CreateAsync(ChannelDto channel, CancellationToken cancellation = default)
        {
            var entity = Mapper.Map<ChannelDto, Channel>(channel);
            var result = await _channelStorage.CreateAsync(entity, cancellation);
            return result.Id;
        }

        public async Task<ChannelDto> FindByIdAsync(long id, CancellationToken cancellation = default)
        {
            var entity = await _channelStorage.FindByIdAsync( id, cancellation);
            return Mapper.Map<Channel, ChannelDto>(entity);
        }

        public async Task<ListResult<ChannelDto>> QueryListAsync(BaseQuery query)
        {
            var result = await _channelStorage.QueryListAsync(query);
            return Mapper.Map<ListResult<Channel>, ListResult<ChannelDto>>(result);
        }

        public async Task<bool> UpdateAsync(ChannelDto channel, CancellationToken cancellation = default)
        {
            var entity = await _channelStorage.FindByIdAsync( channel.Id, cancellation);
            if (entity == null)
            {
                return false;
            }
            Mapper.Map(channel, entity);
            return await _channelStorage.UpdateAsync(entity) > 0;
        }
    }
}