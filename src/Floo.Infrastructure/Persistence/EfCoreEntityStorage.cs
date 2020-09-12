using Floo.Core.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.Infrastructure.Persistence
{
    public class EfCoreEntityStorage<TEntity> : IEntityStorage<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IDbContext _context;
        private readonly IIdentityContext _identityContext;

        public EfCoreEntityStorage(IDbContext context, IIdentityContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
            DbSet = context.Set<TEntity>();
        }

        public DbSet<TEntity> DbSet { get; }

        public TEntity Create(TEntity entity)
        {
            entity.CreatedAtUtc = DateTimeOffset.Now.UtcDateTime;
            entity.CreatedBy = _identityContext.UserId ?? 0;

            this._context.Set<TEntity>().Add(entity);
            return entity;
        }

        public async Task<TEntity> CreateAndSaveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.Create(entity);
            await this._context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            entity.Deleted = true;
            this._context.Set<TEntity>().Update(entity);
        }

        public Task<int> DeleteAndSaveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.Delete(entity);
            return this._context.SaveChangesAsync(cancellationToken);
        }

        public ValueTask<TEntity> FindAsync(CancellationToken cancellationToken = default, params object[] keyValues)
        {
            return this._context.Set<TEntity>().FindAsync(cancellationToken, keyValues);
        }

        public IQueryable<TEntity> Query()
        {
            return this._context.Set<TEntity>();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return this._context.Set<TEntity>().Where(predicate);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return this._context.SaveChangesAsync(cancellationToken);
        }

        public void Update(TEntity entity)
        {
        }

        public Task<int> UpdateAndSaveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return this._context.SaveChangesAsync(cancellationToken);
        }
    }
}