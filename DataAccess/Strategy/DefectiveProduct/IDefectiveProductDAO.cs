namespace DataAccess.Strategy.DefectiveProduct
{
    public interface IDefectiveProductDAO
    {
        List<DAO.DefectiveProduct> GetAll();
        DAO.DefectiveProduct GetById(int id);
        int Insert(DAO.DefectiveProduct defectiveProduct);
        void Update(DAO.DefectiveProduct defectiveProduct);
        void Delete(int id);
        List<DAO.DefectiveProduct> GetByStorage_ID(int storage_ID);
        List<DAO.DefectiveProduct> GetByProduct_ID(int product_ID);
    }
}