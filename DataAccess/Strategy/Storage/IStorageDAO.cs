namespace DataAccess.Strategy.Storage
{
    public interface IStorageDAO
    {
        List<DAO.Storage> GetAll();
        DAO.Storage GetById(int id);
        int Insert(DAO.Storage storage);
        void Update(DAO.Storage storage);
        void Delete(int id);
        List<DAO.Storage> GetByLocation(string location);
    }
}