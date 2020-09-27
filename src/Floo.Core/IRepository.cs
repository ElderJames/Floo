using Floo.App.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.Core
{
    public interface IRepository<TEntity>
       where TEntity : class, IEntity
    {
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<TEntity> FindByIdAsync(long id, CancellationToken cancellationToken = default);

        Task<ListResult<TEntity>> QueryListAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default) where TQuery: BaseQuery;

        Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}