namespace DataAccess.Strategy.Stock
{
    public interface IStockDAO
    {
        List<DAO.Stock> GetAll();
        DAO.Stock GetById(int id);
        int Insert(DAO.Stock stock);
        void Update(DAO.Stock stock);
        void Delete(int id);
        List<DAO.Stock> GetByProduct_ID(int product_ID);
        List<DAO.Stock> GetByStorage_ID(int storage_ID);
    }
}