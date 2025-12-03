using System.Data;
using DataAccess.Database;
using DataAccess.Strategy.Supplier;
using DataAccess.Strategy.Storage;
using DataAccess.Strategy.Product;
using DataAccess.Strategy.Stock;
using DataAccess.Strategy.Company;
using DataAccess.Strategy.Request;
using DataAccess.Strategy.RequestStock;
using DataAccess.Strategy.DefectiveProduct;
using DataAccess.Strategy.User;

namespace DataAccess.UnitOfWork
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly DatabaseConnection _dbConnection;
        private IDbConnection? _connection;
        private IDbTransaction? _transaction;
        private bool _disposed = false;

        private IUserDAO? _users;
        private ISupplierDAO? _suppliers;
        private IStorageDAO? _storages;
        private IProductDAO? _products;
        private IStockDAO? _stocks;
        private ICompanyDAO? _companies;
        private IRequestDAO? _requests;
        private IRequestStockDAO? _requestStocks;
        private IDefectiveProductDAO? _defectiveProducts;

        public SqlUnitOfWork(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = _dbConnection.CreateConnection();
                    _connection.Open();
                }
                return _connection;
            }
        }

        public IDbTransaction? Transaction => _transaction;

        public IUserDAO Users => _users ??= new UserSqlDAO(_dbConnection, this);
        public ISupplierDAO Suppliers => _suppliers ??= new SupplierSqlDAO(_dbConnection, this);
        public IStorageDAO Storages => _storages ??= new StorageSqlDAO(_dbConnection, this);
        public IProductDAO Products => _products ??= new ProductSqlDAO(_dbConnection, this);
        public IStockDAO Stocks => _stocks ??= new StockSqlDAO(_dbConnection, this);
        public ICompanyDAO Companies => _companies ??= new CompanySqlDAO(_dbConnection, this);
        public IRequestDAO Requests => _requests ??= new RequestSqlDAO(_dbConnection, this);
        public IRequestStockDAO RequestStocks => _requestStocks ??= new RequestStockSqlDAO(_dbConnection, this);
        public IDefectiveProductDAO DefectiveProducts => _defectiveProducts ??= new DefectiveProductSqlDAO(_dbConnection, this);

        public void BeginTransaction()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("Transakce již běží.");
            }
            _transaction = Connection.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Žádná transakce k potvrzení.");
            }

            try
            {
                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Žádná transakce k vrácení.");
            }

            try
            {
                _transaction.Rollback();
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _connection?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}