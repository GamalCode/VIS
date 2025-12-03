using DataAccess.Database;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWorkFactory
    {
        private readonly DatabaseConnection _dbConnection;

        public UnitOfWorkFactory(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IUnitOfWork Create()
        {
            return new SqlUnitOfWork(_dbConnection);
        }
    }
}