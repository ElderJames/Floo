using Floo.App.Shared;
using Floo.Core;
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
    public class EfRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly IDbContext _context;
        private readonly IIdentityContext _identityContext;

        public EfRepository(IDbContext context, IIdentityContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
            DbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> DbSet { get; }

        public TEntity Create(TEntity entity)
        {
            entity.CreatedAtUtc = DateTimeOffset.Now.UtcDateTime;
            entity.CreatedBy = _identityContext.UserId ?? 0;

            this._context.Set<TEntity>().Add(entity);
            return entity;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.Create(entity);
            await this._context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            entity.Deleted = true;
            this._context.Set<TEntity>().Update(entity);
        }

        public virtual Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.Delete(entity);
            return this._context.SaveChangesAsync(cancellationToken);
        }

        public virtual Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return this._context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<ListResult<TEntity>> QueryListAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default) where TQuery :BaseQuery
        {
            var linq = this.DbSet;

            HandleConditions(ref linq, query);

            var result = new ListResult<TEntity>(query.Offset, query.Limit);

            if (query.Offset <= 0)
            {
                result.Count = await linq.CountAsync();
            }

            if (query.OrderBy != null && query.OrderBy.Any())
            {
                foreach (var propertyName in query.OrderBy)
                {
                    linq = Sort(linq, propertyName, false) ?? linq;
                }
            }
            else if (query.OrderByDesc != null && query.OrderByDesc.Any())
            {
                foreach (var propertyName in query.OrderByDesc)
                {
                    linq = Sort(linq, propertyName, true) ?? linq;
                }
            }

            result.Items = await linq.Skip(query.Offset).Take(query.Limit).ToListAsync();

            return result;
        }

        public virtual void HandleConditions<TQuery>(ref IQueryable<TEntity> linq, TQuery query) { }

        public virtual IQueryable<TEntity> Sort(IQueryable<TEntity> source, string propertyName, bool isDescending)
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

        public virtual Task<TEntity> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return DbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}