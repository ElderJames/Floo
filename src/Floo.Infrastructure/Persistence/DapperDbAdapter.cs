using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Floo.Core.Shared;

namespace Floo.Infrastructure.Persistence
{
    public class DapperDbAdapter : IDbAdapter
    {
        private readonly Func<IDbConnection> _connectionFactory;

        public DapperDbAdapter(Func<IDbConnection> connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Task<int> ExecuteAsync(string sql, object param) => ExecuteSqlAsync(db => db.ExecuteAsync(sql, param));

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param) => ExecuteSqlAsync(db => db.QueryAsync<T>(sql, param));

        public Task<T> QueryFirstOrDefault<T>(string sql, object param) => ExecuteSqlAsync(db => db.QueryFirstOrDefaultAsync<T>(sql, param));

        public Task<T> ExecuteScalar<T>(string sql, object param) => ExecuteSqlAsync(db => db.ExecuteScalarAsync<T>(sql, param));

        private async Task<T> ExecuteSqlAsync<T>(Func<IDbConnection, Task<T>> action)
        {
            using var conn = _connectionFactory();
            conn.Open();
            return await action(conn);
        }
    }
}