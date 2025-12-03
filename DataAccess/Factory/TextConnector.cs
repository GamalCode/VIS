using DataAccess.Strategy.Supplier;
using DataAccess.Strategy.Storage;
using DataAccess.Strategy.Product;
using DataAccess.Strategy.Stock;
using DataAccess.Strategy.Company;
using DataAccess.Strategy.Request;
using DataAccess.Strategy.RequestStock;
using DataAccess.Strategy.DefectiveProduct;
using DataAccess.Strategy.User;

namespace DataAccess.Factory
{
    public class TextConnector : IDataConnector
    {
        private readonly string _dataPath;

        public TextConnector(string dataPath = "data")
        {
            _dataPath = dataPath;
        }

        public IUserDAO GetUserDAO()
        {
            return new UserTextDAO(_dataPath);
        }

        public ISupplierDAO GetSupplierDAO()
        {
            return new SupplierTextDAO(_dataPath);
        }

        public IStorageDAO GetStorageDAO()
        {
            return new StorageTextDAO(_dataPath);
        }

        public IProductDAO GetProductDAO()
        {
            return new ProductTextDAO(_dataPath);
        }

        public IStockDAO GetStockDAO()
        {
            return new StockTextDAO(_dataPath);
        }

        public ICompanyDAO GetCompanyDAO()
        {
            return new CompanyTextDAO(_dataPath);
        }

        public IRequestDAO GetRequestDAO()
        {
            return new RequestTextDAO(_dataPath);
        }

        public IRequestStockDAO GetRequestStockDAO()
        {
            return new RequestStockTextDAO(_dataPath);
        }

        public IDefectiveProductDAO GetDefectiveProductDAO()
        {
            return new DefectiveProductTextDAO(_dataPath);
        }
    }
}