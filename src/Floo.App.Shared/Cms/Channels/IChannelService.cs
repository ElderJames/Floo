﻿using Floo.Core.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.App.Shared.Cms.Channels
{
    public interface IChannelService
    {
        public Task<ChannelDto> FindAsync(long id, CancellationToken cancellation = default);

        public Task<long> CreateAsync(ChannelDto channel, CancellationToken cancellation = default);

        public Task<bool> UpdateAsync(ChannelDto channel, CancellationToken cancellation = default);

        public Task<ListResult<ChannelDto>> QueryListAsync(BaseQueryDto query);
    }
}