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
    public interface IDataConnector
    {
        IUserDAO GetUserDAO();
        ISupplierDAO GetSupplierDAO();
        IStorageDAO GetStorageDAO();
        IProductDAO GetProductDAO();
        IStockDAO GetStockDAO();
        ICompanyDAO GetCompanyDAO();
        IRequestDAO GetRequestDAO();
        IRequestStockDAO GetRequestStockDAO();
        IDefectiveProductDAO GetDefectiveProductDAO();
    }
}