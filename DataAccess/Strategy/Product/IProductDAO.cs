namespace DataAccess.Strategy.Product
{
    public interface IProductDAO
    {
        List<DAO.Product> GetAll();
        DAO.Product GetById(int id);
        int Insert(DAO.Product product);
        void Update(DAO.Product product);
        void Delete(int id);
        List<DAO.Product> GetBySupplier_ID(int supplier_ID);
        List<DAO.Product> GetByStorage_ID(int storage_ID);
    }
}