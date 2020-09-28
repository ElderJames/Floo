using Floo.App.Shared;
using Floo.App.Shared.Cms.SpecialColumns;
using Floo.Core.Shared.Utils;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.Core.Entities.Cms.Columns
{
    public class ColumnService : IColumnService
    {
        private IColumnRepository _columnStorage;

        public ColumnService(IColumnRepository columnStorage)
        {
            _columnStorage = columnStorage;
        }

        public async Task<long> CreateAsync(ColumnDto specialColumn, CancellationToken cancellation = default)
        {
            var entity = Mapper.Map<ColumnDto, Column>(specialColumn);
            var result = await _columnStorage.CreateAsync(entity, cancellation);
            return result.Id;
        }

        public async Task<ColumnDto> FindByIdAsync(long id, CancellationToken cancellation = default)
        {
            var entity = await _columnStorage.FindByIdAsync(id, cancellation);
            return Mapper.Map<Column, ColumnDto>(entity);
        }

        public async Task<ListResult<ColumnDto>> QueryListAsync(BaseQuery query, CancellationToken cancellation = default)
        {
            var result = await _columnStorage.QueryListAsync(query, cancellation);
            return Mapper.Map<ListResult<Column>, ListResult<ColumnDto>>(result);
        }

        public async Task<bool> UpdateAsync(ColumnDto specialColumn, CancellationToken cancellation = default)
        {
            var entity = await _columnStorage.FindByIdAsync(specialColumn.Id, cancellation);
            if (entity == null)
            {
                return false;
            }
            Mapper.Map(specialColumn, entity);
            return await _columnStorage.UpdateAsync(entity) > 0;
        }
    }
}