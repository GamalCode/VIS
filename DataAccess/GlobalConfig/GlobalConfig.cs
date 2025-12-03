using DataAccess.Database;
using DataAccess.Factory;
using DataAccess.UnitOfWork;

namespace DataAccess.GlobalConfig
{
    public static class GlobalConfig
    {
        public static IDataConnector Connection { get; private set; } = null!;
        public static UnitOfWorkFactory UnitOfWorkFactory { get; private set; } = null!;

        public static void InitializeConnections(DatabaseType db)
        {
            switch (db)
            {
                case DatabaseType.Sql:
                    var dbConnection = new DatabaseConnection();
                    var dbInitializer = new DatabaseInitializer(dbConnection);
                    dbInitializer.Initialize();
                    Connection = new SqlConnector(dbConnection);
                    UnitOfWorkFactory = new UnitOfWorkFactory(dbConnection);
                    break;
                case DatabaseType.TextFile:
                    Connection = new TextConnector();
                    break;
                default:
                    break;
            }
        }
    }

    public enum DatabaseType
    {
        Sql,
        TextFile
    }
}