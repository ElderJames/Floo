using System.Collections.Generic;
using System.Threading.Tasks;

namespace Floo.Core.Shared
{
    public interface IDbAdapter
    {
        Task<int> ExecuteAsync(string sql, object param);

        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param);

        Task<T> QueryFirstOrDefault<T>(string sql, object param);

        Task<T> ExecuteScalar<T>(string sql, object param);
    }
}