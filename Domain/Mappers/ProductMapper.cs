namespace Domain.Mappers
{
    public static class ProductMapper
    {
        public static Models.Product FromDAO(DataAccess.DAO.Product daoProduct)
        {
            if (daoProduct == null) return null;

            return new Models.Product
            {
                Product_ID = daoProduct.Product_ID,
                Name = daoProduct.Name,
                Type = daoProduct.Type,
                CarModel = daoProduct.CarModel,
                Supplier_ID = daoProduct.Supplier_ID,
                Price = daoProduct.Price,
                Storage_ID = daoProduct.Storage_ID
            };
        }

        public static DataAccess.DAO.Product ToDAO(Models.Product domainProduct)
        {
            if (domainProduct == null) return null;

            return new DataAccess.DAO.Product
            {
                Product_ID = domainProduct.Product_ID,
                Name = domainProduct.Name,
                Type = domainProduct.Type,
                CarModel = domainProduct.CarModel,
                Supplier_ID = domainProduct.Supplier_ID,
                Price = domainProduct.Price,
                Storage_ID = domainProduct.Storage_ID
            };
        }

        public static List<Models.Product> FromDAOList(List<DataAccess.DAO.Product> daoProducts)
        {
            return daoProducts?.Select(FromDAO).ToList() ?? new List<Models.Product>();
        }

        public static List<DataAccess.DAO.Product> ToDAOList(List<Models.Product> domainProducts)
        {
            return domainProducts?.Select(ToDAO).ToList() ?? new List<DataAccess.DAO.Product>();
        }
    }
}