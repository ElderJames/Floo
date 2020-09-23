using Floo.Core.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.App.Shared.Cms.SpecialColumns
{
    public interface IColumnService
    {
        public Task<ColumnDto> FindAsync(long id, CancellationToken cancellation = default);

        public Task<long> CreateAsync(ColumnDto specialColumn, CancellationToken cancellation = default);

        public Task<bool> UpdateAsync(ColumnDto specialColumn, CancellationToken cancellation = default);

        public Task<ListResult<ColumnDto>> QueryListAsync(BaseQuery query);
    }
}