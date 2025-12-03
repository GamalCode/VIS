using DataAccess.Strategy.Supplier;
using DataAccess.Strategy.Storage;
using DataAccess.Strategy.Product;
using DataAccess.Strategy.Stock;
using DataAccess.Strategy.Company;
using DataAccess.Strategy.Request;
using DataAccess.Strategy.RequestStock;
using DataAccess.Strategy.DefectiveProduct;
using DataAccess.Strategy.User;
using System.Data;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction? Transaction { get; }

        void BeginTransaction();
        void Commit();
        void Rollback();

        IUserDAO Users { get; }
        ISupplierDAO Suppliers { get; }
        IStorageDAO Storages { get; }
        IProductDAO Products { get; }
        IStockDAO Stocks { get; }
        ICompanyDAO Companies { get; }
        IRequestDAO Requests { get; }
        IRequestStockDAO RequestStocks { get; }
        IDefectiveProductDAO DefectiveProducts { get; }
    }
}