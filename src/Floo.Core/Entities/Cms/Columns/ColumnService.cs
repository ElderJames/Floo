using Floo.App.Shared.Cms.SpecialColumns;
using Floo.Core.Shared;
using Floo.Core.Shared.Utils;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.Core.Entities.Cms.SpecialColumns
{
    public class ColumnService : IColumnService
    {
        private IEntityStorage<Column> _columnStorage;

        public ColumnService(IEntityStorage<Column> columnStorage)
        {
            _columnStorage = columnStorage;
        }

        public async Task<long> CreateAsync(ColumnDto specialColumn, CancellationToken cancellation = default)
        {
            var entity = Mapper.Map<ColumnDto, Column>(specialColumn);
            var result = await _columnStorage.CreateAndSaveAsync(entity, cancellation);
            return result.Id;
        }

        public async Task<ColumnDto> FindAsync(long id, CancellationToken cancellation = default)
        {
            var entity = await _columnStorage.FindAsync(cancellation, id);
            return Mapper.Map<Column, ColumnDto>(entity);
        }

        public async Task<ListResult<ColumnDto>> QueryListAsync(BaseQuery query)
        {
            var result = await _columnStorage.QueryAsync(query);
            return Mapper.Map<ListResult<Column>, ListResult<ColumnDto>>(result);
        }

        public async Task<bool> UpdateAsync(ColumnDto specialColumn, CancellationToken cancellation = default)
        {
            var entity = await _columnStorage.FindAsync(cancellation, specialColumn.Id);
            if (entity == null)
            {
                return false;
            }
            Mapper.Map(specialColumn, entity);
            return await _columnStorage.UpdateAndSaveAsync(entity) > 0;
        }
    }
}