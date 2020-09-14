using Floo.Core.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.App.Shared.Cms.SpecialColumns
{
    public interface ISpecialColumnService
    {
        public Task<SpecialColumnDto> FindAsync(long id, CancellationToken cancellation = default);

        public Task<long> CreateAsync(SpecialColumnDto specialColumn, CancellationToken cancellation = default);

        public Task<bool> UpdateAsync(SpecialColumnDto specialColumn, CancellationToken cancellation = default);

        public Task<ListResult<SpecialColumnDto>> QueryListAsync(BaseQuery query);
    }
}