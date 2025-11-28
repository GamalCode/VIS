namespace DataAccess.Strategy.Supplier
{
    public interface ISupplierDAO
    {
        List<DAO.Supplier> GetAll();
        DAO.Supplier GetById(int id);
        int Insert(DAO.Supplier supplier);
        void Update(DAO.Supplier supplier);
        void Delete(int id);
        List<DAO.Supplier> GetByName(string name);
        List<DAO.Supplier> GetByStorage_ID(int storage_ID);
    }
}