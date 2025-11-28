namespace DataAccess.Strategy.RequestStock
{
    public interface IRequestStockDAO
    {
        List<DAO.RequestStock> GetAll();
        DAO.RequestStock GetById(int id);
        int Insert(DAO.RequestStock requestStock);
        void Update(DAO.RequestStock requestStock);
        void Delete(int id);
        List<DAO.RequestStock> GetByRequest_ID(int request_ID);
        List<DAO.RequestStock> GetByStock_ID(int stock_ID);
    }
}