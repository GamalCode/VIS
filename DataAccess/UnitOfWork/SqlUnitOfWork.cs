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

        public IUserDAO Users => _users ??= new UserSqlDAO(_dbConnection);
        public ISupplierDAO Suppliers => _suppliers ??= new SupplierSqlDAO(_dbConnection);
        public IStorageDAO Storages => _storages ??= new StorageSqlDAO(_dbConnection);
        public IProductDAO Products => _products ??= new ProductSqlDAO(_dbConnection);
        public IStockDAO Stocks => _stocks ??= new StockSqlDAO(_dbConnection);
        public ICompanyDAO Companies => _companies ??= new CompanySqlDAO(_dbConnection);
        public IRequestDAO Requests => _requests ??= new RequestSqlDAO(_dbConnection);
        public IRequestStockDAO RequestStocks => _requestStocks ??= new RequestStockSqlDAO(_dbConnection);
        public IDefectiveProductDAO DefectiveProducts => _defectiveProducts ??= new DefectiveProductSqlDAO(_dbConnection);

        public void BeginTransaction() { }

        public void Commit() { }

        public void Rollback() { }

        public void Dispose() { }
    }
}