using Floo.App.Shared.Cms.SpecialColumns;
using Floo.Core.Shared;
using Floo.Core.Shared.Utils;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.Core.Entities.Cms.SpecialColumns
{
    public class SpecialColumnService : ISpecialColumnService
    {
        private IEntityStorage<SpecialColumn> _specialColumnStorage;

        public SpecialColumnService(IEntityStorage<SpecialColumn> specialColumnStorage)
        {
            _specialColumnStorage = specialColumnStorage;
        }

        public async Task<long> CreateAsync(SpecialColumnDto specialColumn, CancellationToken cancellation = default)
        {
            var entity = Mapper.Map<SpecialColumnDto, SpecialColumn>(specialColumn);
            var result = await _specialColumnStorage.CreateAndSaveAsync(entity, cancellation);
            return result.Id;
        }

        public async Task<SpecialColumnDto> FindAsync(long id, CancellationToken cancellation = default)
        {
            var entity = await _specialColumnStorage.FindAsync(cancellation, id);
            return Mapper.Map<SpecialColumn, SpecialColumnDto>(entity);
        }

        public async Task<ListResult<SpecialColumnDto>> QueryListAsync(BaseQuery query)
        {
            var result = await _specialColumnStorage.QueryAsync(query);
            return Mapper.Map<ListResult<SpecialColumn>, ListResult<SpecialColumnDto>>(result);
        }

        public async Task<bool> UpdateAsync(SpecialColumnDto specialColumn, CancellationToken cancellation = default)
        {
            var entity = await _specialColumnStorage.FindAsync(cancellation, specialColumn.Id);
            if (entity == null)
            {
                return false;
            }
            Mapper.Map(specialColumn, entity);
            return await _specialColumnStorage.UpdateAndSaveAsync(entity) > 0;
        }
    }
}