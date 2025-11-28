namespace DataAccess.Strategy.Company
{
    public interface ICompanyDAO
    {
        List<DAO.Company> GetAll();
        DAO.Company GetById(int id);
        int Insert(DAO.Company company);
        void Update(DAO.Company company);
        void Delete(int id);
        List<DAO.Company> GetByName(string name);
        List<DAO.Company> GetByStorage_ID(int storage_ID);
    }
}