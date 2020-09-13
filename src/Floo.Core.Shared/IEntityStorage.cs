﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.Core.Shared
{
    public interface IEntityStorage<TEntity>
       where TEntity : class, IEntity
    {
        TEntity Create(TEntity entity);

        Task<TEntity> CreateAndSaveAsync(TEntity entity, CancellationToken cancellationToken = default);

        void Delete(TEntity entity);

        Task<int> DeleteAndSaveAsync(TEntity entity, CancellationToken cancellationToken = default);

        ValueTask<TEntity> FindAsync(CancellationToken cancellationToken = default, params object[] keyValues);

        IQueryable<TEntity> Query();

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);

        Task<ListResult<TEntity>> QueryAsync(BaseQueryDto query, Func<IQueryable<TEntity>, Expression<Func<TEntity, bool>>> predicate = null);

        void Update(TEntity entity);

        Task<int> UpdateAndSaveAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}