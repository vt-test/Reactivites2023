using System.Data;

namespace Persistence
{
    public interface IDataAccess
    {
        IDbConnection CreateConnection(string connectionId);
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameteres, string connectionId = "Default");
        Task SaveData<T>(string storedProcedure, T parameteres, string connectionId = "Default");
        Task<IEnumerable<T>> SQL<T, U>(string sql, U parameteres, string connectionId = "Default");
        Task<IEnumerable<T>> SQL_Multi<T, U>(string sql, U parameteres, string connectionId = "Default");
    }
}