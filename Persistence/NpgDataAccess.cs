using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
using System.Data;

namespace Persistence
{
    public class NpgDataAccess : IDataAccess
    {
        private readonly IConfiguration _config;

        public NpgDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameteres, string connectionId = "Default")
        {
            using IDbConnection connection = new NpgsqlConnection(_config.GetConnectionString(connectionId));

            return await connection.QueryAsync<T>(storedProcedure, parameteres, commandType: CommandType.StoredProcedure);
        }

        // mozemo koristiti i ovu funkciju za kreiranje connection stringa
        public IDbConnection CreateConnection(string connectionId) => new NpgsqlConnection(_config.GetConnectionString(connectionId));


        public async Task SaveData<T>(string storedProcedure, T parameteres, string connectionId = "Default")
        {
            using IDbConnection connection = new NpgsqlConnection(_config.GetConnectionString(connectionId));

            await connection.ExecuteAsync(storedProcedure, parameteres, commandType: CommandType.StoredProcedure);
        }


        public async Task<IEnumerable<T>> SQL<T, U>(string sql, U parameteres, string connectionId = "Default")
        {
            using IDbConnection connection = new NpgsqlConnection(_config.GetConnectionString(connectionId));

            return await connection.QueryAsync<T>(sql, parameteres, commandType: CommandType.Text);
        }

        public async Task<IEnumerable<T>> SQL_Multi<T, U>(string sql, U parameteres, string connectionId = "Default")
        {
            using IDbConnection connection = new NpgsqlConnection(_config.GetConnectionString(connectionId));

            var multi = await connection.QueryMultipleAsync(sql, parameteres, commandType: CommandType.Text);

            multi.Read<T>().ToList();
            var kor2 = multi.Read<T>().ToList();

            return kor2;
        }
    }
}
