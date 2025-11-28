namespace DataAccess.Strategy.Request
{
    public interface IRequestDAO
    {
        List<DAO.Request> GetAll();
        DAO.Request GetById(int id);
        int Insert(DAO.Request request);
        void Update(DAO.Request request);
        void Delete(int id);
        List<DAO.Request> GetByCompany_ID(int company_ID);
        List<DAO.Request> GetByStatus(string status);
    }
}