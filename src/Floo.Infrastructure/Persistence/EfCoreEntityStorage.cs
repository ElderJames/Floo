using Floo.Core.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
            return this._context.Set<TEntity>().FindAsync(keyValues, cancellationToken);
        }

        public IQueryable<TEntity> Query()
        {
            return this._context.Set<TEntity>();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return this._context.Set<TEntity>().Where(predicate);
        }

        public async Task<ListResult<TEntity>> QueryAsync(BaseQuery query, Action<IQueryable<TEntity>> linqAction = null)
        {
            var linq = this._context.Set<TEntity>().AsQueryable();
            if (linqAction != null)
            {
                linqAction.Invoke(linq);
            }

            var result = new ListResult<TEntity>(query.Offset, query.Limit);

            if (query.Offset <= 0)
            {
                result.Count = await linq.CountAsync();
            }

            if (query.OrderBy.Any())
            {
                foreach (var propertyName in query.OrderBy)
                {
                    linq = Sort(linq, propertyName, false) ?? linq;
                }
            }
            else if (query.OrderByDesc.Any())
            {
                foreach (var propertyName in query.OrderByDesc)
                {
                    linq = Sort(linq, propertyName, true) ?? linq;
                }
            }

            result.Items = await linq.Skip(query.Offset).Take(query.Limit).ToListAsync();

            return result;
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

        public IQueryable<TEntity> Sort(IQueryable<TEntity> source, string propertyName, bool isDescending)
        {
            var type = typeof(TEntity);
            PropertyInfo prop = type.GetProperty(propertyName);

            if (prop == null)
            {
                return default;
            }

            Type funcType = typeof(Func<,>).MakeGenericType(type, prop.PropertyType);

            MethodInfo lambdaBuilder = typeof(Expression).GetMethods()
                .First(x => x.Name == "Lambda" && x.ContainsGenericParameters && x.GetParameters().Length == 2)
                .MakeGenericMethod(funcType);

            ParameterExpression parameter = Expression.Parameter(type);
            MemberExpression propExpress = Expression.Property(parameter, prop);

            var sortLambda = lambdaBuilder.Invoke(null, new object[] { propExpress, new[] { parameter } });

            MethodInfo sorter = typeof(Queryable).GetMethods()
                .FirstOrDefault(
                    x => x.Name == (isDescending ? "OrderByDescending" : "OrderBy") && x.GetParameters().Length == 2)
                .MakeGenericMethod(type, prop.PropertyType);

            return (IQueryable<TEntity>)sorter.Invoke(null, new[] { source, sortLambda });
        }
    }
}