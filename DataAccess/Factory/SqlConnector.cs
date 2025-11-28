using DataAccess.Database;
using DataAccess.Strategy.Supplier;
using DataAccess.Strategy.Storage;
using DataAccess.Strategy.Product;
using DataAccess.Strategy.Stock;
using DataAccess.Strategy.Company;
using DataAccess.Strategy.Request;
using DataAccess.Strategy.RequestStock;
using DataAccess.Strategy.DefectiveProduct;

namespace DataAccess.Factory
{
    public class SqlConnector : IDataConnector
    {
        private readonly DatabaseConnection _dbConnection;

        public SqlConnector(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public ISupplierDAO GetSupplierDAO()
        {
            return new SupplierSqlDAO(_dbConnection);
        }

        public IStorageDAO GetStorageDAO()
        {
            return new StorageSqlDAO(_dbConnection);
        }

        public IProductDAO GetProductDAO()
        {
            return new ProductSqlDAO(_dbConnection);
        }

        public IStockDAO GetStockDAO()
        {
            return new StockSqlDAO(_dbConnection);
        }

        public ICompanyDAO GetCompanyDAO()
        {
            return new CompanySqlDAO(_dbConnection);
        }

        public IRequestDAO GetRequestDAO()
        {
            return new RequestSqlDAO(_dbConnection);
        }

        public IRequestStockDAO GetRequestStockDAO()
        {
            return new RequestStockSqlDAO(_dbConnection);
        }

        public IDefectiveProductDAO GetDefectiveProductDAO()
        {
            return new DefectiveProductSqlDAO(_dbConnection);
        }
    }
}