namespace Domain.Mappers
{
    public static class DefectiveProductMapper
    {
        public static Models.DefectiveProduct FromDAO(DataAccess.DAO.DefectiveProduct daoDefectiveProduct)
        {
            if (daoDefectiveProduct == null) return null;

            return new Models.DefectiveProduct
            {
                Defective_ID = daoDefectiveProduct.Defective_ID,
                Product_ID = daoDefectiveProduct.Product_ID,
                Storage_ID = daoDefectiveProduct.Storage_ID,
                Quantity = daoDefectiveProduct.Quantity,
                Report_Date = daoDefectiveProduct.Report_Date,
                Reason = daoDefectiveProduct.Reason
            };
        }

        public static DataAccess.DAO.DefectiveProduct ToDAO(Models.DefectiveProduct domainDefectiveProduct)
        {
            if (domainDefectiveProduct == null) return null;

            return new DataAccess.DAO.DefectiveProduct
            {
                Defective_ID = domainDefectiveProduct.Defective_ID,
                Product_ID = domainDefectiveProduct.Product_ID,
                Storage_ID = domainDefectiveProduct.Storage_ID,
                Quantity = domainDefectiveProduct.Quantity,
                Report_Date = domainDefectiveProduct.Report_Date,
                Reason = domainDefectiveProduct.Reason
            };
        }

        public static List<Models.DefectiveProduct> FromDAOList(List<DataAccess.DAO.DefectiveProduct> daoDefectiveProducts)
        {
            return daoDefectiveProducts?.Select(FromDAO).ToList() ?? new List<Models.DefectiveProduct>();
        }

        public static List<DataAccess.DAO.DefectiveProduct> ToDAOList(List<Models.DefectiveProduct> domainDefectiveProducts)
        {
            return domainDefectiveProducts?.Select(ToDAO).ToList() ?? new List<DataAccess.DAO.DefectiveProduct>();
        }
    }
}