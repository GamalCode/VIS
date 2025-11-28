using System.Data;
using Microsoft.Data.Sqlite;

namespace DataAccess.Database
{
    public class DatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection(string databasePath = "warehouse.db")
        {
            _connectionString = $"Data Source={databasePath};";
        }

        public IDbConnection CreateConnection()
        {
            return new SqliteConnection(_connectionString);
        }

        public string ConnectionString => _connectionString;
    }
}