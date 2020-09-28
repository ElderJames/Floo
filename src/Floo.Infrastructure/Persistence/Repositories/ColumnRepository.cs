using Floo.Core.Entities.Cms.Columns;
using Floo.Core.Shared;
using System.Linq;

namespace Floo.Infrastructure.Persistence.Repositories
{
    public class ColumnRepository : EfRepository<Column>, IColumnRepository
    {
        public ColumnRepository(IDbContext context, IIdentityContext identityContext) : base(context, identityContext) { }

        public override void HandleConditions<TQuery>(ref IQueryable<Column> linq, TQuery query)
        {
        }
    }
}
