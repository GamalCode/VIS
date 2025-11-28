namespace Domain.Mappers
{
    public static class SupplierMapper
    {
        public static Models.Supplier FromDAO(DataAccess.DAO.Supplier daoSupplier)
        {
            if (daoSupplier == null) return null;

            return new Models.Supplier
            {
                Supplier_ID = daoSupplier.Supplier_ID,
                Name = daoSupplier.Name,
                Phone = daoSupplier.Phone,
                Email = daoSupplier.Email,
                Storage_ID = daoSupplier.Storage_ID
            };
        }

        public static DataAccess.DAO.Supplier ToDAO(Models.Supplier domainSupplier)
        {
            if (domainSupplier == null) return null;

            return new DataAccess.DAO.Supplier
            {
                Supplier_ID = domainSupplier.Supplier_ID,
                Name = domainSupplier.Name,
                Phone = domainSupplier.Phone,
                Email = domainSupplier.Email,
                Storage_ID = domainSupplier.Storage_ID
            };
        }

        public static List<Models.Supplier> FromDAOList(List<DataAccess.DAO.Supplier> daoSuppliers)
        {
            return daoSuppliers?.Select(FromDAO).ToList() ?? new List<Models.Supplier>();
        }

        public static List<DataAccess.DAO.Supplier> ToDAOList(List<Models.Supplier> domainSuppliers)
        {
            return domainSuppliers?.Select(ToDAO).ToList() ?? new List<DataAccess.DAO.Supplier>();
        }
    }
}